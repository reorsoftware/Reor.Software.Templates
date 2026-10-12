using Microsoft.AspNetCore.Routing;
using PROJECT_NS.PROJECT.Presentation.Documentation;
using PROJECT_NS.PROJECT.Presentation.Greetings.Endpoints;

namespace PROJECT_NS.PROJECT.Presentation;

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