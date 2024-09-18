using BicycleSharingSystem.WebApi.Models;

using Microsoft.EntityFrameworkCore;

namespace BicycleSharingSystem.WebApi.Contexts;

/// <summary>
/// Bicycle Sharing Context.
/// </summary>
public sealed class BicycleSharingContext : DbContext
{
    public BicycleSharingContext(DbContextOptions<BicycleSharingContext> dbContextOptions)
        : base(dbContextOptions)
    {
        Database.EnsureCreated();
        Database.Migrate();
    }

    /// <summary>
    /// Rental Offices
    /// </summary>
    public DbSet<RentalOfficeModel> RentalOffices { get; init; }

    /// <summary>
    /// Bicycles.
    /// </summary>
    public DbSet<BicycleModel> Bicycles { get; init; }
}