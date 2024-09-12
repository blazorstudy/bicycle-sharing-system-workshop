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
    public DbSet<RentalOfficeModel> RentalOffices { get; init; }

    /// <summary>
    /// Bicycles.
    /// </summary>
    public DbSet<BicycleModel> Bicycles { get; init; }
}