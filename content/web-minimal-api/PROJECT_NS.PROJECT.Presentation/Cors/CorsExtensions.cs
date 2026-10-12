using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace PROJECT_NS.PROJECT.Presentation.Cors;

public static class CorsExtensions
{
    public static void AddReorApiCors(this IServiceCollection services, Action<CorsOptions>? configureOptions = null)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.SetIsOriginAllowed(origin =>
                    {
                        if (!Uri.TryCreate(origin, UriKind.Absolute, out var uri)) return false;
                        return uri.IsLocalhost() || uri.IsProductionSite();
                    })
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
            
            configureOptions?.Invoke(options);
        });
    }
    
    extension(Uri uri)
    {
        private bool IsLocalhost() => (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps) 
                                      && uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase);

        private bool IsProductionSite() => uri.Scheme == Uri.UriSchemeHttps 
                                           && uri.Host.Equals("PROJECT.reor.software", StringComparison.OrdinalIgnoreCase) 
                                           && uri.IsDefaultPort;
    }
}