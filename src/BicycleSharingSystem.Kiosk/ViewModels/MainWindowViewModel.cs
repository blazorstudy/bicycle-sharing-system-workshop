using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BicycleSharingSystem.Kiosk.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentPage;
    
    private readonly ViewModelBase[] Pages =
    {
        new HomeViewModel(),
        new RentalOfficeViewModel(),
        new BikeViewModel(),
    };
    public MainWindowViewModel()
    {
        this.CurrentPage = Pages[0];
    }

    [RelayCommand]
    private void HomeButton()
    {
        this.CurrentPage = Pages[0];
    }
    [RelayCommand]
    private void RentalOfficeButton()
    {
        this.CurrentPage = Pages[1];
    }
}
