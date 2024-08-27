using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace BicycleSharingSystem.WebApi.Models;

/// <summary>
/// Bicycle Model.
/// </summary>
[PrimaryKey(nameof(BicycleId))]
[Table(nameof(BicycleModel))]
public sealed class BicycleModel
{
    /// <summary>
    /// Bicycle ID.
    /// </summary>
    public required Guid BicycleId { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Rental Office Name.
    /// </summary>
    public string RentalOfficeName { get; set; } = null!;

    /// <summary>
    /// Is Rental.
    /// </summary>
    [ConcurrencyCheck]
    public bool IsRental { get; set; }
}