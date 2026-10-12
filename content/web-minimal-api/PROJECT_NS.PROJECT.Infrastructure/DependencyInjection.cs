using Microsoft.Extensions.DependencyInjection;
using PROJECT_NS.PROJECT.Domain.Services;
using PROJECT_NS.PROJECT.Infrastructure.Persistence;
using PROJECT_NS.PROJECT.Infrastructure.Services;

namespace PROJECT_NS.PROJECT.Infrastructure;

public static class DependencyInjection
{
    public class InfrastructureOptions
    {
    }
    
    public static IServiceCollection AddPROJECTInfrastructure(this IServiceCollection services, InfrastructureOptions? options = null)
    {
        options ??= new InfrastructureOptions();
        
        services.AddOptions<PROJECTDbContextConfig>().BindConfiguration("Database");
        services.AddDbContext<PROJECTDbContext>();

        services.AddScoped<IGreetingService, RecognisingGreetingService>();
        
        return services;
    }
}