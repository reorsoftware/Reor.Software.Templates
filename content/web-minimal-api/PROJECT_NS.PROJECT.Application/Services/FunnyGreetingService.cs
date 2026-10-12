using System.Text;
using PROJECT_NS.PROJECT.Domain.Entities;
using PROJECT_NS.PROJECT.Domain.Services;

namespace PROJECT_NS.PROJECT.Application.Services;

public class FunnyGreetingService : IGreetingService
{
    public async Task<string> BuildGreetingAsync(GreetingParticipants participants)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{participants.Greeter}: Knock knock");
        sb.AppendLine($"{participants.Greetee}: Who's there?");
        sb.AppendLine($"{participants.Greeter}: {participants.Greetee}");
        sb.AppendLine($"{participants.Greetee}: {participants.Greetee} who");
        sb.AppendLine($"{participants.Greeter}: you");
        return sb.ToString();
    }
}