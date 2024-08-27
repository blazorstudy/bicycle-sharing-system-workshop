using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace BicycleSharingSystem.WebApi.Models;

/// <summary>
/// Rental Office Model.
/// </summary>
[PrimaryKey(nameof(Name))]
[Table(nameof(RentalOfficeModel))]
public sealed class RentalOfficeModel
{
    /// <summary>
    /// Rental Office Name.
    /// </summary>
    public required string Name { get; init; } = null!;
}