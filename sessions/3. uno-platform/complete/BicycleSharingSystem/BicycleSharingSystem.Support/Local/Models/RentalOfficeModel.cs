namespace BicycleSharingSystem.Support.Local.Models;

public class RentalOfficeModel
{
    public Guid OfficeId { get; set; }
    public string Name { get; set; }
    public string Region { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public List<BicycleModel> Bicycles { get; set; }
}
