using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BicycleSharingSystem.Kiosk.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static

    public MainWindowViewModel()
    {
    }

    [RelayCommand]
    private void HomeButton()
    {
        Console.WriteLine(Greeting);
    }
    [RelayCommand]
    private void RentalOfficeButton()
    {
        Console.WriteLine(Greeting);
    }
}
