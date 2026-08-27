using Reor.Software.PROJECT.Application;
using Reor.Software.PROJECT.Domain;
using Reor.Software.PROJECT.Infrastructure;
using Reor.Software.PROJECT.Presentation;
using Reor.Software.PROJECT.Presentation.Documentation;
using DependencyInjection = Reor.Software.PROJECT.Presentation.DependencyInjection;

namespace Reor.Software.PROJECT.WebHost;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddAuthorization();
        builder.Services.AddOpenApi();
        builder.Services.AddLogging();
        
        builder.Services.AddPROJECTDomain();
        builder.Services.AddPROJECTApplication();
        builder.Services.AddPROJECTInfrastructure();
        
        var app = builder.Build();
        var presentationOptions = new DependencyInjection.PresentationOptions();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            presentationOptions.EnabledDocumentationVendors = DocumentationExtensions.DocumentationVendors.All;
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        
        app.MapGet("/", () => "Hello World! This is PROJECT!");
        app.MapPROJECTPresentation(presentationOptions);

        app.Run();
    }
}