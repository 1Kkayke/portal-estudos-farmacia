using PortalEstudos.API.Data;
using PortalEstudos.API.Extensions;
using PortalEstudos.API.Security;
using PortalEstudos.API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var isProduction = builder.Environment.IsProduction();

// Configuração de serviços organizadas por extensões
builder.Services
    .AddDatabaseConfiguration(builder.Configuration, isProduction)
    .AddIdentityConfiguration()
    .AddJwtAuthentication(builder.Configuration, isProduction)
    .AddApplicationServices()
    .AddPerformanceOptimizations()
    .AddCorsConfiguration(builder.Configuration)
    .AddSecurityConfiguration();

// Controllers e API
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCustomRateLimiting(builder.Configuration);

var app = builder.Build();

// Pipeline de middleware
await ConfigureMiddlewarePipelineAsync(app);

// Inicialização do banco de dados
await InitializeDatabaseAsync(app);

// Executar aplicação
app.Run();

static async Task ConfigureMiddlewarePipelineAsync(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseForwardedHeaders();
    app.UseResponseCompression();
    app.UseResponseCaching();

    if (!app.Environment.IsDevelopment())
    {
        app.UseHsts();
    }

    app.UseSecurityHeaders();
    app.UseStaticFiles();
    app.UseCors("AllowFrontend");
    app.UseRateLimiter();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}

static async Task InitializeDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    try
    {
        await context.Database.MigrateAsync();
        
        // Fazer seeding apenas em desenvolvimento
        if (app.Environment.IsDevelopment())
        {
            StudyContentSeeder.EnsureSeeded(context);
        }
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro durante inicialização do banco de dados");
        throw;
    }
}
