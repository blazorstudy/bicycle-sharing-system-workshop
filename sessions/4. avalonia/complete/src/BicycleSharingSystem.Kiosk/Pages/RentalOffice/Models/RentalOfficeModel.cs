using BicycleSharingSystem.Kiosk.Queries;

namespace BicycleSharingSystem.Kiosk.Pages.RentalOffice.Models;

public class RentalOfficeModel
{
    public string Number { get; set; }
    public string RegionName { get; set; }
    public string Name { get; set; }

    public RentalOfficeModel(RentalOfficeDTO dto)
    {
        Number = dto.OfficeId;
        Name = dto.Name;
        RegionName = dto.Region;
    }
}