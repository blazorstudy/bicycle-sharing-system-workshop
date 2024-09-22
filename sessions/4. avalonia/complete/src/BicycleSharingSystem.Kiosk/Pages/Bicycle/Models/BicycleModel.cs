using BicycleSharingSystem.Kiosk.Queries;

using CommunityToolkit.Mvvm.ComponentModel;

namespace BicycleSharingSystem.Kiosk.Pages.Bicycle.Models;

public partial class BicycleModel : ObservableObject
{
    public string Name { get; set; }
    public string StartRentalTime { get; set; }
    public string RemainDateTime { get; set; }
    [ObservableProperty] private bool isRental = false;

    public BicycleModel(BicycleDTO dto)
    {
        this.Name = dto.Name;
        this.IsRental = dto.IsRental;
        if (this.IsRental)
        {
            this.StartRentalTime = dto.StartRentalTime.Value.ToString("HH:mm");
            TimeSpan timeDiff = dto.ExpireRentalTime.Value - DateTime.Now;
            this.RemainDateTime = $"{timeDiff.Minutes.ToString("D2")}:{timeDiff.Seconds.ToString("D2")}";
        }
    }
}