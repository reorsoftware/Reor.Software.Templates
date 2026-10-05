using Reor.Software.Auth;
using Reor.Software.PROJECT.Application;
using Reor.Software.PROJECT.Domain;
using Reor.Software.PROJECT.Infrastructure;
using Reor.Software.PROJECT.Presentation;
using Reor.Software.PROJECT.Presentation.Cors;
using Reor.Software.PROJECT.Presentation.Documentation;
using DependencyInjection = Reor.Software.PROJECT.Presentation.DependencyInjection;

namespace Reor.Software.PROJECT.WebHost;

public class Program
{
    private const string API_CLAIM = "PROJECT.api";
    
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        
        
        builder.Services.AddReorApiAuthentication();
        builder.Services.AddReorApiAuthorization("PROJECTApi", API_CLAIM, PolicyMode.All);
        builder.Services.AddReorApiCors();
        builder.Services.AddOpenApi(options => options.UseReorSecurityTransformer([API_CLAIM]));

        builder.Services.AddProblemDetails();
        builder.Services.AddLogging();
        
        builder.Services.AddPROJECTDomain();
        builder.Services.AddPROJECTApplication();
        builder.Services.AddPROJECTInfrastructure();
        
        var app = builder.Build();
        var presentationOptions = new DependencyInjection.PresentationOptions();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi().AllowAnonymous();
            presentationOptions.EnabledDocumentationVendors = DocumentationExtensions.DocumentationVendors.All;
        }

        app.UseHttpsRedirection();
        app.UseCors();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapGet("/", () => "Hello World! This is PROJECT!");
        app.MapPROJECTPresentation(presentationOptions);

        app.Run();
    }
}