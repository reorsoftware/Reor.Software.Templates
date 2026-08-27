using Microsoft.Extensions.DependencyInjection;
using Reor.Software.PROJECT.Application.Services;
using Reor.Software.PROJECT.Domain.Services;

namespace Reor.Software.PROJECT.Application;

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