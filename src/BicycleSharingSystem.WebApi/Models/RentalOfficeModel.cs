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
    public Guid OfficeId { get; init; } = Guid.NewGuid();

    /// <summary>
    /// 대여소 이름
    /// </summary>
    [Required]
    [MaxLength(100)]
    public required string Name { get; init; } = null!;
}