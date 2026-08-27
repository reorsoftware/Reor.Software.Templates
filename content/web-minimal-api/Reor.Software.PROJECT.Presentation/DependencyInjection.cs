using Microsoft.AspNetCore.Routing;
using Reor.Software.PROJECT.Presentation.Documentation;
using Reor.Software.PROJECT.Presentation.Greetings.Endpoints;

namespace Reor.Software.PROJECT.Presentation;

public static class DependencyInjection
{
    public class PresentationOptions
    {
        public string DocumentationEndpointPrefix { get; init; } = "/docs";
        public DocumentationExtensions.DocumentationVendors EnabledDocumentationVendors { get; set; } =
            DocumentationExtensions.DocumentationVendors.All;
    }
    
    public static IEndpointRouteBuilder MapPROJECTPresentation(this IEndpointRouteBuilder routes, PresentationOptions? options = null)
    {
        options ??= new PresentationOptions();
        routes.AddDocumentationEndpoints(
            options.DocumentationEndpointPrefix, 
            options.EnabledDocumentationVendors
        );

        routes.MapGreetingEndpoints();
        
        return routes;
    }
}