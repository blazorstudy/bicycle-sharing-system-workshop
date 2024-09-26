using BicycleSharingSystem.Support.Local.Services;
using Jamesnet.Core;

namespace BicycleSharingSystem.Navigate.Local.Services;

public class MenuManager : IMenuManager
{
    private readonly IContainer _container;

    public event Action<string> MenuChanged;

    public MenuManager(IContainer container)
    {
        _container = container;
    }

    public void ChangeMenu(string menu)
    {
        MenuChanged?.Invoke(menu);
    }

    public void NavigateTo(string menu)
    {
        var layerManager = _container.Resolve<ILayerManager>();
        var contentView = _container.Resolve<IView>($"{menu}Content");
        layerManager.Show("ContentLayer", contentView);
    }
}
