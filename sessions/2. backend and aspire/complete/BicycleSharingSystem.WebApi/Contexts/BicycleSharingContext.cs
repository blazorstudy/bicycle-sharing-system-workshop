using BicycleSharingSystem.WebApi.Models;

using Microsoft.EntityFrameworkCore;

namespace BicycleSharingSystem.WebApi.Contexts;

/// <summary>
/// Bicycle Sharing Context.
/// </summary>
public sealed class BicycleSharingContext(DbContextOptions<BicycleSharingContext> dbContextOptions)
    : DbContext(dbContextOptions)
{
    /// <summary>
    /// Rental Offices
    /// </summary>
    public DbSet<RentalOfficeModel> RentalOffices => Set<RentalOfficeModel>();

    /// <summary>
    /// Bicycles.
    /// </summary>
    public DbSet<BicycleModel> Bicycles => Set<BicycleModel>();

    /// <summary>
    /// Do any database initialization required.
    /// </summary>
    /// <returns>A task that completes when the database is initialized</returns>
    public async Task InitializeDatabaseAsync()
    {
        await Database.EnsureCreatedAsync().ConfigureAwait(false);
    }
}