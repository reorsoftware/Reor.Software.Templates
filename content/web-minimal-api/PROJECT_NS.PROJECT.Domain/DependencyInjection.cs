using Microsoft.Extensions.DependencyInjection;

namespace PROJECT_NS.PROJECT.Domain;

public static class DependencyInjection
{
    public class DomainOptions
    {
        
    }
    
    public static IServiceCollection AddPROJECTDomain(this IServiceCollection services, DomainOptions? options = null)
    {
        options ??= new DomainOptions();
        
        // services.AddSingleton<IMyService, MyServiceImplementation>();
        
        return services;
    }
}