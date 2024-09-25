﻿using System.ComponentModel.DataAnnotations;

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

    /// <summary>
    /// 대여소 지역(위치)
    /// </summary>
    [Required]
    [MaxLength(100)]
    public required string Region { get; init; } = null!;

    /// <summary>
    /// 위도 (Latitude)
    /// </summary>
    public double? Latitude { get; init; }

    /// <summary>
    /// 경도 (Longitude)
    /// </summary>
    public double? Longitude { get; init; }
}