namespace Reor.Software.PROJECT.Infrastructure.Entities;

public class GreetingEvent : IPROJECTEntity
{
    public Guid Id { get; set; }
    
    public string Greetee { get; set; }
    public string Greeter { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}