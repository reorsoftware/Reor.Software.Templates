using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Reor.Software.PROJECT.Presentation.Documentation;

public static class DocumentationExtensions
{
    [Flags]
    public enum DocumentationVendors
    {
        None = 0,
        
        Scalar =  1 << 0,
        Swagger = 1 << 1,
        ReDoc =   1 << 2,
        
        All = Scalar | Swagger | ReDoc
    }

    public static IEndpointRouteBuilder AddDocumentationEndpoints(
        this IEndpointRouteBuilder routes,
        string pathPrefix = "/docs",
        DocumentationVendors vendors = DocumentationVendors.All)
    {
        var docGroup = routes
            .MapGroup(pathPrefix)
            .ExcludeFromDescription();
        
        if (vendors.HasFlag(DocumentationVendors.Scalar)) docGroup.AddScalar();
        if (vendors.HasFlag(DocumentationVendors.Swagger)) docGroup.AddSwagger();
        if (vendors.HasFlag(DocumentationVendors.Scalar)) docGroup.AddReDoc();

        return routes;
    }
    
    private static IEndpointRouteBuilder AddScalar(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/scalar", () =>
        {
            const string html = """
                                <!doctype html>
                                <html>
                                  <head>
                                    <title>PROJECT Scalar API</title>
                                    <meta charset="utf-8" />
                                    <meta name="viewport" content="width=device-width, initial-scale=1" />
                                  </head>
                                  <body>
                                    <script
                                      id="api-reference"
                                      data-url="/openapi/v1.json"></script>
                                    <script src="https://cdn.jsdelivr.net/npm/@scalar/api-reference"></script>
                                  </body>
                                </html>
                                """;

            return TypedResults.Text(html, "text/html");
        });
        return routes;
    }
    
    private static IEndpointRouteBuilder AddSwagger(this IEndpointRouteBuilder routes)
    {
        return routes;
    }
    
    private static IEndpointRouteBuilder AddReDoc(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/redoc", () =>
        {
            const string html = """
                                <!DOCTYPE html>
                                <html>
                                  <head>
                                    <title>PROJECT ReDoc API</title>
                                    <meta charset="utf-8"/>
                                    <meta name="viewport" content="width=device-width, initial-scale=1">
                                    <link href="https://fonts.googleapis.com/css?family=Montserrat:300,400,700|Roboto:300,400,700" rel="stylesheet">

                                    <style>
                                      body {
                                        margin: 0;
                                        padding: 0;
                                      }
                                    </style>
                                  </head>
                                  <body>
                                    <redoc spec-url='/openapi/v1.json'></redoc>
                                    <script src="https://cdn.redoc.ly/redoc/latest/bundles/redoc.standalone.js"> </script>
                                  </body>
                                </html>
                                """;

            return TypedResults.Text(html, "text/html");
        });
        return routes;
    }
}