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
        if (_config.UseSqlite)
        {
            var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dir = Path.Combine(dbPath, "Reor.Software.PROJECT");
            var path = Path.Combine(dir, "PROJECT.db");
            Console.Out.WriteLine($"DB in path: {path}");
            
            try
            {
                Directory.CreateDirectory(dir);

                var conStr = _config.ConnectionString ?? $"Filename={path}";
                optionsBuilder.UseSqlite(conStr);
            }
            catch
            {
                throw new Exception("FAILED TO CONNECT TO DATABASE FILE FOR SQLITE");
            }
        }
        else if (_config.UsePostgreSql)
        {
            var conStr = _config.ConnectionString;
            optionsBuilder.UseNpgsql(conStr);
        }
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