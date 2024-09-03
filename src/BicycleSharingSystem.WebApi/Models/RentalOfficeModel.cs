using System.ComponentModel.DataAnnotations;

namespace BicycleSharingSystem.WebApi.Models;

/// <summary>
/// Rental Office Model.
/// </summary>
public sealed class RentalOfficeModel
{
    /// <summary>
    /// Office ID.
    /// </summary>
    [Key]
    public required Guid OfficeId { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Rental Office Name.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public required string Name { get; init; } = null!;
}