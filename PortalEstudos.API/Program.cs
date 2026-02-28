using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortalEstudos.API.Data;
using PortalEstudos.API.Middleware;
using PortalEstudos.API.Models;
using PortalEstudos.API.Security;
using PortalEstudos.API.Services;
using System.Text;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var isProduction = builder.Environment.IsProduction();
var defaultConn = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=PortalEstudos.db";
var envConn = builder.Configuration["DB_CONNECTION_STRING"] ?? builder.Configuration["DATABASE_URL"];
var connectionString = string.IsNullOrWhiteSpace(envConn) ? defaultConn : envConn;
var usePostgres = IsPostgresConnectionString(connectionString);

if (usePostgres)
{
    connectionString = NormalizePostgresConnectionString(connectionString);
}

if (isProduction && string.IsNullOrWhiteSpace(envConn) && !usePostgres)
{
    var writableDbPath = Path.Combine(Path.GetTempPath(), "PortalEstudos.db");
    connectionString = $"Data Source={writableDbPath}";
}

// ===== 1. Banco de Dados (PostgreSQL/SQLite via EF Core) =====
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (usePostgres)
    {
        options.UseNpgsql(connectionString, npgsql =>
        {
            npgsql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        });
        return;
    }

    options.UseSqlite(connectionString);
});

// ===== 2. ASP.NET Core Identity =====
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Configurações de senha simplificadas para desenvolvimento
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configurar expiração de token de recuperação de senha para 24 horas
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(24);
});

// ===== 3. Autenticação JWT =====
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("JWT Key não configurada no appsettings.json");

var envJwtKey = builder.Configuration["JWT_SECRET_KEY"];
if (!string.IsNullOrWhiteSpace(envJwtKey))
{
    jwtKey = envJwtKey;
}

if (isProduction && jwtKey.Contains("ChaveSecretaPortalEstudosFarmacia", StringComparison.OrdinalIgnoreCase))
{
    throw new InvalidOperationException("JWT_SECRET_KEY deve ser configurada por variável de ambiente em produção.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// ===== 4. Serviços da aplicação =====
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddSingleton<DocumentPdfService>();
builder.Services.AddSingleton<ApostilaHtmlService>();
builder.Services.AddHttpClient<TranslationService>();
builder.Services.AddHttpClient<NewsFeedService>()
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        AutomaticDecompression = System.Net.DecompressionMethods.All
    });

// ===== 5. Controllers e Swagger =====
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCustomRateLimiting(builder.Configuration);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

// ===== 6. CORS — permite chamadas do frontend React (porta 5173) =====
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        var configuredOrigins = builder.Configuration["CORS_ALLOWED_ORIGINS"];
        var origins = string.IsNullOrWhiteSpace(configuredOrigins)
            ? new[] { "http://localhost:5173", "http://localhost:5174", "http://localhost:3000" }
            : configuredOrigins.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        policy.WithOrigins(origins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// ===== Middleware Pipeline =====
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseForwardedHeaders();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

// Não fazer UseHttpsRedirection() em produção (Render já providencia HTTPS via reverse proxy)
// app.UseHttpsRedirection();

app.UseSecurityHeaders();

// Servir arquivos estáticos (imagens de perfil, etc.)
app.UseStaticFiles();

app.UseCors("AllowFrontend");
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// ===== Aplicar Migrations no startup =====
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (usePostgres)
    {
        db.Database.EnsureCreated();
    }
    else
    {
        db.Database.Migrate();
    }
    
    // Fazer seeding apenas em desenvolvimento
    if (app.Environment.IsDevelopment())
    {
        StudyContentSeeder.EnsureSeeded(db);
    }
}

app.Run();

static bool IsPostgresConnectionString(string connectionString)
{
    if (string.IsNullOrWhiteSpace(connectionString))
        return false;

    var normalized = connectionString.Trim().ToLowerInvariant();
    return normalized.StartsWith("postgres://")
           || normalized.StartsWith("postgresql://")
           || normalized.Contains("host=")
           || normalized.Contains("username=")
           || normalized.Contains("userid=")
           || normalized.Contains("database=");
}

static string NormalizePostgresConnectionString(string connectionString)
{
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Connection string do PostgreSQL inválida.");

    var trimmed = connectionString.Trim();
    if (!(trimmed.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase)
          || trimmed.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase)))
    {
        return trimmed;
    }

    var uri = new Uri(trimmed);
    var userInfo = uri.UserInfo.Split(':', 2);
    var username = Uri.UnescapeDataString(userInfo[0]);
    var password = userInfo.Length > 1 ? Uri.UnescapeDataString(userInfo[1]) : string.Empty;
    var database = uri.AbsolutePath.Trim('/');

    var builder = new NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.Port > 0 ? uri.Port : 5432,
        Username = username,
        Password = password,
        Database = database,
        SslMode = SslMode.Require,
        Pooling = true
    };

    return builder.ConnectionString;
}
