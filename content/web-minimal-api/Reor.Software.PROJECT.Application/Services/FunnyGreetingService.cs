using System.Text;
using Reor.Software.PROJECT.Domain.Entities;
using Reor.Software.PROJECT.Domain.Services;

namespace Reor.Software.PROJECT.Application.Services;

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