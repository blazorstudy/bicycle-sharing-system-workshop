using BicycleSharingSystem.Kiosk.Queries;
using BicycleSharingSystem.Kiosk.ViewModels.MessengerModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace BicycleSharingSystem.Kiosk.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentPage;

    private ViewModelBase[] Pages;
    public MainWindowViewModel(IRentalOfficeQuery rentalOfficeQuery,
                               IBicycleQuery bicycleQuery)
    {
        Pages = new ViewModelBase[]{
            new HomeViewModel(),
            new RentalOfficeViewModel(rentalOfficeQuery),
            new BicycleViewModel(bicycleQuery),
        };
        this.CurrentPage = Pages[0];
        
        WeakReferenceMessenger.Default.Register<SelectRentalOfficeChangeMessage>(this, (r, m) =>
        {
            var model = m.Value;

            this.CurrentPage = Pages[2];
            
            ((BicycleViewModel)this.CurrentPage).Init((model));
        });
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
