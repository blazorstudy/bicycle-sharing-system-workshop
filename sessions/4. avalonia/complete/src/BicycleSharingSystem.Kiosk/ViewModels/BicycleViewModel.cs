using BicycleSharingSystem.Kiosk.Pages.Bicycle.Models;
using BicycleSharingSystem.Kiosk.Pages.RentalOffice.Models;
using BicycleSharingSystem.Kiosk.Queries;

using CommunityToolkit.Mvvm.ComponentModel;

namespace BicycleSharingSystem.Kiosk.ViewModels;

public partial class BicycleViewModel : ViewModelBase
{
    [ObservableProperty] private List<BicycleModel> bicycles;
    [ObservableProperty] private string renterShopName;
    private readonly IBicycleQuery _bicycleQuery;
    public BicycleViewModel(IBicycleQuery bicycleQuery)
    {
        this._bicycleQuery = bicycleQuery;
        this.Bicycles = new();
    }
    public async Task  Init(RentalOfficeModel model)
    {
        this.RenterShopName = model.Name;
        this.Bicycles.Clear();
        foreach (var item in await this._bicycleQuery.Get(model.Number))
        {
            this.Bicycles.Add(new BicycleModel(item));
        }
    }
}