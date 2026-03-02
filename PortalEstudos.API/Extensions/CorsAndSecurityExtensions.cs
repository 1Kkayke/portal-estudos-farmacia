using Microsoft.AspNetCore.HttpOverrides;

namespace PortalEstudos.API.Extensions
{
    public static class CorsAndSecurityExtensions
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    var configuredOrigins = configuration["CORS_ALLOWED_ORIGINS"];
                    var explicitOrigins = string.IsNullOrWhiteSpace(configuredOrigins)
                        ? Array.Empty<string>()
                        : configuredOrigins.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                    var allowedOrigins = new HashSet<string>(explicitOrigins, StringComparer.OrdinalIgnoreCase);

                    policy.SetIsOriginAllowed(origin => IsOriginAllowed(origin, allowedOrigins))
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            return services;
        }

        public static IServiceCollection AddSecurityConfiguration(this IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            return services;
        }

        private static bool IsOriginAllowed(string origin, HashSet<string> allowedOrigins)
        {
            if (string.IsNullOrWhiteSpace(origin))
                return false;

            if (allowedOrigins.Contains(origin))
                return true;

            if (!Uri.TryCreate(origin, UriKind.Absolute, out var uri))
                return false;

            var host = uri.Host.ToLowerInvariant();
            var isLocalhost = host == "localhost" || host == "127.0.0.1";
            var isPreviewHost = host.EndsWith(".vercel.app", StringComparison.OrdinalIgnoreCase)
                                || host.EndsWith(".netlify.app", StringComparison.OrdinalIgnoreCase);

            return isLocalhost || isPreviewHost;
        }
    }
}