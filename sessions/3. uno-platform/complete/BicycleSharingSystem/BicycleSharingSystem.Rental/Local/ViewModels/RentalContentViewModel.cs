using BicycleSharingSystem.Support.Local.Models;
using BicycleSharingSystem.Support.Local.Services;
using Jamesnet.Core;

namespace BicycleSharingSystem.Rental.Local.ViewModels;

public class RentalContentViewModel : ViewModelBase
{
    private readonly IMenuManager _menu;
    private readonly IRentalOfficeService _rentalService;
    private RentalOfficeModel _selectedRental;
    private List<RentalOfficeModel> _rentals;

    [CanExecute("UpdateRentalCommand")]
    [CanExecute("DeleteRentalCommand")]
    [CanExecute("BicycleCommand")]
    public RentalOfficeModel SelectedRental
    {
        get => _selectedRental;
        set => SetProperty(ref _selectedRental, value);
    }

    public List<RentalOfficeModel> Rentals
    {
        get => _rentals;
        set => SetProperty(ref _rentals, value);
    }

    public ICommand LoadRentalsCommand { get; }
    public ICommand AddRentalCommand { get; }
    public ICommand UpdateRentalCommand { get; }
    public ICommand DeleteRentalCommand { get; }
    public ICommand BicycleCommand { get; set; }

    public RentalContentViewModel(IMenuManager menu, IRentalOfficeService rentalService)
    {
        _menu = menu;
        _rentalService = rentalService;

        LoadRentalsCommand = new RelayCommand(LoadRentals);
        AddRentalCommand = new RelayCommand(AddRental);
        UpdateRentalCommand = new RelayCommand(UpdateRental, () => SelectedRental != null);
        DeleteRentalCommand = new RelayCommand(DeleteRental, () => SelectedRental != null);
        BicycleCommand = new RelayCommand(Bicycle, () => SelectedRental != null);

        LoadRentals();
    }

    private async void LoadRentals()
    {
        Rentals = await _rentalService.GetAllRentalOfficesAsync();
    }

    private async void AddRental()
    {
        RentalOfficeModel newRentalOffice = new()
        {
            OfficeId = Guid.NewGuid(),
            Name = $"New Office {DateTime.Now.Ticks}",
            Region = "Default Region",
            Latitude = 0,
            Longitude = 0
        };

        await _rentalService.AddRentalOfficesAsync([newRentalOffice]);
        LoadRentals();
    }

    private async void UpdateRental()
    {
        await _rentalService.UpdateRentalOfficeAsync(SelectedRental);
        LoadRentals();
    }

    private async void DeleteRental()
    {
        await _rentalService.DeleteRentalOfficeAsync(SelectedRental.Name);
        LoadRentals();
    }

    private void Bicycle()
    {
        _menu.ChangeMenu("Bicycle");
    }
}
