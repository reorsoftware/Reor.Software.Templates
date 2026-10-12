using Microsoft.Extensions.DependencyInjection;
using PROJECT_NS.PROJECT.Application.Services;
using PROJECT_NS.PROJECT.Domain.Services;

namespace PROJECT_NS.PROJECT.Application;

public static class DependencyInjection
{
    public class ApplicationOptions
    {
        
    }
    
    public static IServiceCollection AddPROJECTApplication(this IServiceCollection services, ApplicationOptions? options = null)
    {
        options ??= new ApplicationOptions();

        services.AddSingleton<IGreetingService, FunnyGreetingService>();
        services.AddSingleton<IGreetingService, SimpleGreetingService>();
        // services.AddSingleton<IMyService, MyServiceImplementation>();
        
        return services;
    }
}