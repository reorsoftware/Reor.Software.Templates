using Reor.Software.PROJECT.Domain.Entities;

namespace Reor.Software.PROJECT.Domain.Services;

public interface IGreetingService
{
    /// <summary>
    /// Build a greeting message
    /// </summary>
    /// <param name="participants">The collection with the name of the greeter and greetee</param>
    /// <returns>A greeting message</returns>
    public Task<string> BuildGreetingAsync(GreetingParticipants participants);
}