using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid BicycleId { get; init; }

    /// <summary>
    /// 대여소 이름
    /// </summary>
    [Required]
    public required Guid RentalOfficeId { get; set; }

    /// <summary>
    /// 자전거 이름
    /// </summary>
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; } = null!;

    /// <summary>
    /// 대여 시작일
    /// </summary>
    public DateTime? StartRentalTime { get; set; }

    /// <summary>
    /// 반납 예정일 또는 마지막 반납일
    /// </summary>
    public DateTime? ExpireRentalTime { get; set; }

    /// <summary>
    /// 대여 중 여부
    /// </summary>
    public bool IsRental => StartRentalTime.HasValue;
}