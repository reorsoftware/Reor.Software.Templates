using Microsoft.Extensions.DependencyInjection;
using Reor.Software.PROJECT.Domain.Services;
using Reor.Software.PROJECT.Infrastructure.Persistence;
using Reor.Software.PROJECT.Infrastructure.Services;

namespace Reor.Software.PROJECT.Infrastructure;

public static class DependencyInjection
{
    public class InfrastructureOptions
    {
    }
    
    public static IServiceCollection AddPROJECTInfrastructure(this IServiceCollection services, InfrastructureOptions? options = null)
    {
        options ??= new InfrastructureOptions();

        services.AddOptions<PROJECTDbContextConfig>("Database");
        services.AddDbContext<PROJECTDbContext>();

        services.AddScoped<IGreetingService, RecognisingGreetingService>();
        
        return services;
    }
}