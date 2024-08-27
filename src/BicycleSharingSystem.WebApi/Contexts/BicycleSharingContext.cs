using BicycleSharingSystem.WebApi.Contexts.Bases;
using BicycleSharingSystem.WebApi.Models;

using Microsoft.EntityFrameworkCore;

namespace BicycleSharingSystem.WebApi.Contexts;

/// <summary>
/// Bicycle Sharing Context.
/// </summary>
public sealed class BicycleSharingContext : DatabaseContextBase
{
    private const string DbFileName = "bicycle_sharing.db";

    /// <summary>
    /// Initialize <see cref="BicycleSharingContext"/> Instance.
    /// </summary>
    public BicycleSharingContext() : base(DbFileName)
    {
        Database.EnsureCreated();
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