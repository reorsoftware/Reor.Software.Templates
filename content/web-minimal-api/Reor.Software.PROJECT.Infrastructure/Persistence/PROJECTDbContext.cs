using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Reor.Software.PROJECT.Domain.Entities;
using Reor.Software.PROJECT.Infrastructure.Entities;

namespace Reor.Software.PROJECT.Infrastructure.Persistence;

public class PROJECTDbContext : DbContext
{
    private readonly PROJECTDbContextConfig _config;
    
    public PROJECTDbContext(
        DbContextOptions<PROJECTDbContext> options,
        IOptions<PROJECTDbContextConfig> config) : base(options)
    {
        _config = config.Value;
    }
    
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        HandlePROJECTEntities();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        HandlePROJECTEntities();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (string.IsNullOrWhiteSpace(_config.ConnectionString))
            throw new InvalidOperationException("A PostgreSQL connection string must be configured.");

        optionsBuilder.UseNpgsql(_config.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PROJECTDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    private void HandlePROJECTEntities()
    {
        var entities = ChangeTracker.Entries<IPROJECTEntity>();
        
        foreach (var entity in entities)
        {
            switch (entity.State)
            {
                case EntityState.Added:
                    entity.Entity.CreatedAt = DateTimeOffset.Now;
                    entity.Entity.UpdatedAt = DateTimeOffset.Now;
                    break;
                case EntityState.Modified:
                    entity.Entity.UpdatedAt = DateTimeOffset.Now;
                    break;
                case EntityState.Detached:
                case EntityState.Unchanged:
                case EntityState.Deleted:
                default:
                    break;
            }
        }
    }
}