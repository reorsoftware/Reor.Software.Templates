namespace Reor.Software.PROJECT.Infrastructure.Entities;

public interface IPROJECTEntity
{
    public Guid Id { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}