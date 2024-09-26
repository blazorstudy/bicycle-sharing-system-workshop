using BicycleSharingSystem.Support.Local.Models;
using BicycleSharingSystem.Support.Local.Services;
using Jamesnet.Core;

namespace BicycleSharingSystem.Bicycle.Local.ViewModels;

public class BicycleContentViewModel : ViewModelBase
{
    private readonly IBicycleSharingService _bicycleService;
    private readonly IRentalOfficeService _rentalService;

    private RentalOfficeModel _selectedRentalOffice;
    private BicycleModel _selectedBicycle;
    private List<RentalOfficeModel> _rentalOffices;
    private List<BicycleModel> _bicycles;

    [CanExecute("AddBicycleCommand")]
    public RentalOfficeModel SelectedRentalOffice
    {
        get => _selectedRentalOffice;
        set => SetProperty(ref _selectedRentalOffice, value, LoadBicyclesForSelectedOffice);
    }

    [CanExecute("UpdateBicycleCommand")]
    [CanExecute("DeleteBicycleCommand")]
    public BicycleModel SelectedBicycle
    {
        get => _selectedBicycle;
        set => SetProperty(ref _selectedBicycle, value);
    }

    public List<RentalOfficeModel> RentalOffices
    {
        get => _rentalOffices;
        set => SetProperty(ref _rentalOffices, value);
    }

    public List<BicycleModel> Bicycles
    {
        get => _bicycles;
        set => SetProperty(ref _bicycles, value);
    }

    public ICommand LoadRentalOfficesCommand { get; }
    public ICommand AddBicycleCommand { get; }
    public ICommand UpdateBicycleCommand { get; }
    public ICommand DeleteBicycleCommand { get; }

    public BicycleContentViewModel(IBicycleSharingService bicycleService, IRentalOfficeService rentalService)
    {
        _bicycleService = bicycleService;
        _rentalService = rentalService;

        LoadRentalOfficesCommand = new RelayCommand(LoadRentalOffices);
        AddBicycleCommand = new RelayCommand(AddBicycle, () => SelectedRentalOffice != null);
        UpdateBicycleCommand = new RelayCommand(UpdateBicycle, () => SelectedBicycle != null);
        DeleteBicycleCommand = new RelayCommand(DeleteBicycle, () => SelectedBicycle != null);

        LoadRentalOffices();
    }

    private async void LoadRentalOffices()
    {
        RentalOffices = await _rentalService.GetAllRentalOfficesAsync();
    }

    private async void LoadBicyclesForSelectedOffice()
    {
        if(SelectedRentalOffice == null) return;    

        var office = await _rentalService.GetRentalOfficeAsync(SelectedRentalOffice.Name);
        Bicycles = office.Bicycles;
    }

    private async void AddBicycle()
    {
        BicycleModel newBicycle = new()
        {
            BicycleId = Guid.NewGuid(),
            Name = $"New Bicycle {DateTime.Now.Ticks}",
            RentalOfficeId = SelectedRentalOffice.OfficeId,
            StartRentalTime = DateTime.Now,
            ExpireRentalTime = DateTime.Now.AddDays(1)
        };

        await _bicycleService.AddBicyclesAsync([newBicycle]);
        LoadBicyclesForSelectedOffice();
    }

    private async void UpdateBicycle()
    {
        await _bicycleService.UpdateBicycleAsync(SelectedBicycle.BicycleId, SelectedBicycle);
        LoadBicyclesForSelectedOffice();
    }

    private async void DeleteBicycle()
    {
        await _bicycleService.DeleteBicycleAsync(SelectedBicycle.BicycleId);
        LoadBicyclesForSelectedOffice();
    }
}
