using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Reor.Software.PROJECT.Domain.Entities;
using Reor.Software.PROJECT.Domain.Services;
using Reor.Software.PROJECT.Presentation.Greetings.Models;

namespace Reor.Software.PROJECT.Presentation.Greetings.Endpoints;

public static class GreetingEndpoints
{
    internal static IEndpointRouteBuilder MapGreetingEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("/greeting")
            .WithTags("greeting");

        group.MapPost("", async (
                IEnumerable<IGreetingService> services,
                GreetingRequest request
            ) =>
            {
                if (string.IsNullOrWhiteSpace(request.Greeter))
                    return Results.UnprocessableEntity("Greeter cannot be empty.");
                if (string.IsNullOrWhiteSpace(request.Greetee))
                    return Results.UnprocessableEntity("Greetee cannot be empty.");

                var participants = new GreetingParticipants()
                {
                    Greeter = request.Greetee,
                    Greetee = request.Greetee
                };
                var serviceList = services.ToList();
                var service = serviceList[Random.Shared.Next(serviceList.Count)];

                var greeting = await service.BuildGreetingAsync(participants);
                var response = new GreetingResponse()
                {
                    Greetee = request.Greetee,
                    Greeter = request.Greeter,
                    GreetingMessage = greeting
                };
                return Results.Ok(response);
            })
            .Produces<string>(StatusCodes.Status422UnprocessableEntity)
            .Produces<GreetingResponse>(StatusCodes.Status200OK);
        
        return endpoints;
    }
}