namespace Reor.Software.PROJECT.Infrastructure.Persistence;

public class PROJECTDbContextConfig
{
    public bool UseSqlite { get; set; } = true;
    public bool UsePostgreSql { get; set; } = false;
    public string? ConnectionString { get; set; } = null;
}