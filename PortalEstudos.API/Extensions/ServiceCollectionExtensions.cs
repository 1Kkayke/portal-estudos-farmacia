using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortalEstudos.API.Data;
using PortalEstudos.API.Models;
using PortalEstudos.API.Services;
using System.Text;
using Npgsql;

namespace PortalEstudos.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseConfiguration(
            this IServiceCollection services, 
            IConfiguration configuration, 
            bool isProduction)
        {
            var defaultConn = configuration.GetConnectionString("DefaultConnection") ?? "Data Source=PortalEstudos.db";
            var envConn = configuration["DB_CONNECTION_STRING"] ?? configuration["DATABASE_URL"];
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

            services.AddDbContext<ApplicationDbContext>(options =>
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

            return services;
        }

        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(24);
            });

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services, 
            IConfiguration configuration, 
            bool isProduction)
        {
            var jwtKey = configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT Key não configurada no appsettings.json");

            var envJwtKey = configuration["JWT_SECRET_KEY"];
            if (!string.IsNullOrWhiteSpace(envJwtKey))
            {
                jwtKey = envJwtKey;
            }

            if (isProduction && jwtKey.Contains("ChaveSecretaPortalEstudosFarmacia", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("JWT_SECRET_KEY deve ser configurada por variável de ambiente em produção.");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<TokenService>();
            services.AddScoped<EmailService>();
            services.AddSingleton<DocumentPdfService>();
            services.AddSingleton<ApostilaHtmlService>();
            services.AddSingleton<PremiumApostilaHtmlService>();
            
            services.AddHttpClient<TranslationService>();
            services.AddHttpClient<NewsFeedService>()
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.All
                });

            return services;
        }

        public static IServiceCollection AddPerformanceOptimizations(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.GzipCompressionProvider>();
                options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.BrotliCompressionProvider>();
                options.MimeTypes = Microsoft.AspNetCore.ResponseCompression.ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/json", "text/json", "text/plain", "application/xml" }).ToArray();
                options.EnableForHttps = true;
            });

            services.AddResponseCaching();
            return services;
        }

        private static bool IsPostgresConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                return false;

            var normalized = connectionString.ToLowerInvariant();
            return normalized.StartsWith("postgres://")
                   || normalized.StartsWith("postgresql://")
                   || normalized.Contains("host=")
                   || normalized.Contains("username=")
                   || normalized.Contains("userid=")
                   || normalized.Contains("database=");
        }

        private static string NormalizePostgresConnectionString(string connectionString)
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
    }
}