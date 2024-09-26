using BicycleSharingSystem.Support.Local.Services;
using Jamesnet.Core;

namespace BicycleSharingSystem.Main.Local.ViewModels;
public class MainContentViewModel : IViewLoadable
{
    private readonly IMenuManager _menu;

    public MainContentViewModel(IMenuManager menu)
    {
        _menu = menu;
    }

    public void Loaded()
    {
        _menu.ChangeMenu("Bicycle");
    }
}
