namespace BicycleSharingSystem.Support.Local.Models;

public class BicycleModel
{
    public Guid BicycleId { get; set; }

    public Guid RentalOfficeId { get; set; }

    public string Name { get; set; }

    public DateTime? StartRentalTime { get; set; }

    public DateTime? ExpireRentalTime { get; set; }

    public bool IsRental => StartRentalTime.HasValue;
}
