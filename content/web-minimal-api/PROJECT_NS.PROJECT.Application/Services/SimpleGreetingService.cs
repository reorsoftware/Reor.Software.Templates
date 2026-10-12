using PROJECT_NS.PROJECT.Domain.Entities;
using PROJECT_NS.PROJECT.Domain.Services;

namespace PROJECT_NS.PROJECT.Application.Services;

public class SimpleGreetingService : IGreetingService
{
    public async Task<string> BuildGreetingAsync(GreetingParticipants participants)
    {
        return $"{participants.Greeter}: Hello {participants.Greetee}";
    }
}