using System.ComponentModel.DataAnnotations;

namespace BicycleSharingSystem.WebApi.Models;

/// <summary>
/// Bicycle Model.
/// </summary>
public sealed class BicycleModel
{
    /// <summary>
    /// Bicycle ID.
    /// </summary>
    [Key]
    public required Guid BicycleId { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Rental Office Name.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public required string RentalOfficeName { get; set; } = null!;

    /// <summary>
    /// Is Rental.
    /// </summary>
    public bool IsRental { get; set; }

    /// <summary>
    /// Rental Start DateTime.
    /// </summary>
    public DateTime? StartRentalTime { get; set; }

    /// <summary>
    /// Expire Rental DateTime.
    /// </summary>
    public DateTime? ExpireRentalTime { get; set; }
}