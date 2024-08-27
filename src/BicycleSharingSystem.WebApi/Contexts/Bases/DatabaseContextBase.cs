using Microsoft.EntityFrameworkCore;

namespace BicycleSharingSystem.WebApi.Contexts.Bases;

/// <summary>
/// Database Context Base
/// </summary>
public abstract class DatabaseContextBase(string dbName) : DbContext
{
    /// <summary>
    /// Database File Path
    /// </summary>
    public string DatabasePath { get; } = Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            dbName);

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DatabasePath}");
}