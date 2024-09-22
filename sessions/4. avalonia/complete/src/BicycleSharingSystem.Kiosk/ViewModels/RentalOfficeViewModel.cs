using System.Collections.ObjectModel;
using System.ComponentModel;

using BicycleSharingSystem.Kiosk.Pages.RentalOffice.Models;
using BicycleSharingSystem.Kiosk.Queries;
using BicycleSharingSystem.Kiosk.ViewModels.MessengerModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace BicycleSharingSystem.Kiosk.ViewModels;

public partial class RentalOfficeViewModel: ViewModelBase
{
    [ObservableProperty] ObservableCollection<RentalOfficeModel> rentalOffices;
    [ObservableProperty] ObservableCollection<RentalOfficeModel> filterRentalOffices;
    [ObservableProperty] ObservableCollection<string> regions;
    [ObservableProperty] string selectedRegions;
    private readonly IRentalOfficeQuery _rentalOfficeQuery;
    public RentalOfficeViewModel(IRentalOfficeQuery rentalOfficeQuery)
    {
        _rentalOfficeQuery = rentalOfficeQuery;
        this.RentalOffices = new();
        this.Regions = new();
        this.FilterRentalOffices = new();
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName == "SelectedRegions")
        {
            if (SelectedRegions == "전체")
            {
                this.FilterRentalOffices = RentalOffices;
            }
            else
            {
                this.FilterRentalOffices = new (RentalOffices.Where(x=>x.RegionName == SelectedRegions).ToList());
            }
        }
    }

    public override async Task Load()
    {
        RentalOffices.Clear();
        Regions.Clear();
        foreach (var item in await this._rentalOfficeQuery.Get())
        {
            RentalOffices.Add(new RentalOfficeModel(item));
        }
        
        Regions = new(RentalOffices
            .GroupBy(p => p.RegionName)
            .Select(g => g.Key).ToList());
        
        Regions.Insert(0, "전체");

        SelectedRegions = Regions.First();
    }

    [RelayCommand]
    private void SelectedRentalOffice(RentalOfficeModel model)
    {
        WeakReferenceMessenger.Default.Send(new SelectRentalOfficeChangeMessage(model));
    }
}