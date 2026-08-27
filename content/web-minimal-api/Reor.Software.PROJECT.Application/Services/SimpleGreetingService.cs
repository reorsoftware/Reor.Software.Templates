using Reor.Software.PROJECT.Domain.Entities;
using Reor.Software.PROJECT.Domain.Services;

namespace Reor.Software.PROJECT.Application.Services;

public class SimpleGreetingService : IGreetingService
{
    public async Task<string> BuildGreetingAsync(GreetingParticipants participants)
    {
        return $"{participants.Greeter}: Hello {participants.Greetee}";
    }
}