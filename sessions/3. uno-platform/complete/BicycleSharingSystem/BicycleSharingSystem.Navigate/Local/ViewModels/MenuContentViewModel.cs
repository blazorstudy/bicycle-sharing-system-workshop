using BicycleSharingSystem.Support.Local.Services;
using Jamesnet.Core;

namespace BicycleSharingSystem.Navigate.Local.ViewModels;

public class MenuContentViewModel : ViewModelBase
{
    private readonly IMenuManager _menuManager;
    private bool _isRentalSelected;
    private bool _isBicycleSelected;

    public bool IsRentalSelected
    {
        get => _isRentalSelected;
        set => SetProperty(ref _isRentalSelected, value);
    }

    public bool IsBicycleSelected
    {
        get => _isBicycleSelected;
        set => SetProperty(ref _isBicycleSelected, value);
    }

    public ICommand NavigateCommand { get; init; }

    public MenuContentViewModel(IMenuManager menuManager)
    {
        _menuManager = menuManager;
        _menuManager.MenuChanged += OnMenuChanged;
        NavigateCommand = new RelayCommand<string>(NavigateToMenu);
    }

    private void OnMenuChanged(string menu)
    {
        switch (menu)
        {
            case "Rental":
                IsRentalSelected = true;
                IsBicycleSelected = false;
                break;
            case "Bicycle":
                IsRentalSelected = false;
                IsBicycleSelected = true;
                break;
            default:
                IsRentalSelected = false;
                IsBicycleSelected = false;
                break;
        }

        NavigateToMenu(menu);
    }

    private void NavigateToMenu(string menu)
    {
        _menuManager.NavigateTo(menu);
    }
}
