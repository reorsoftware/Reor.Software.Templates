using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reor.Software.PROJECT.Domain.Entities;
using Reor.Software.PROJECT.Domain.Services;
using Reor.Software.PROJECT.Infrastructure.Entities;
using Reor.Software.PROJECT.Infrastructure.Persistence;

namespace Reor.Software.PROJECT.Infrastructure.Services;

public class RecognisingGreetingService : IGreetingService
{
    private readonly PROJECTDbContext _context;
    private readonly ILogger<RecognisingGreetingService> _logger;

    public RecognisingGreetingService(
        PROJECTDbContext context, 
        ILogger<RecognisingGreetingService> logger
        )
    {
        _context = context;
        _logger = logger;
    }

    public async Task<string> BuildGreetingAsync(GreetingParticipants participants)
    {
        var newGreeting = new GreetingEvent()
        {
            Greetee = participants.Greetee,
            Greeter = participants.Greeter,
        };
        try
        {
            var previousAppearances = await _context
                .Set<GreetingEvent>()
                .Where(g => g.Greetee == participants.Greetee)
                .ToListAsync();
            
            _context.Add(newGreeting);
            await _context.SaveChangesAsync();
            
            if (previousAppearances.Count == 0) return $"{participants.Greeter}: We haven't seen you before {participants.Greetee}";

            return $"{participants.Greeter}: Ah yes. We've seen you before {participants.Greetee}";

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create greeting event");
            return $"{participants.Greeter}: I've lost my pen sorry";
        }
    }
}