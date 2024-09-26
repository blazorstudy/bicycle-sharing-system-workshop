using BicycleSharingSystem.Bicycle.Local.ViewModels;
using BicycleSharingSystem.Bicycle.UI.Views;
using BicycleSharingSystem.Main.Local.ViewModels;
using BicycleSharingSystem.Main.UI.Views;
using BicycleSharingSystem.Navigate.Local.Services;
using BicycleSharingSystem.Navigate.Local.ViewModels;
using BicycleSharingSystem.Navigate.UI.Views;
using BicycleSharingSystem.Rental.Local.ViewModels;
using BicycleSharingSystem.Rental.UI.Views;
using BicycleSharingSystem.Support.Local.Services;
using Jamesnet.Core;

namespace BicycleSharingSystem;
internal class BicycleSharingSystemBootstrapper : AppBootstrapper
{
    protected override void RegisterViewModels(IViewModelMapper vmMapper)
    {
        vmMapper.Register<MainContent, MainContentViewModel>();
        vmMapper.Register<MenuContent, MenuContentViewModel>();
        vmMapper.Register<BicycleContent, BicycleContentViewModel>();
        vmMapper.Register<RentalContent, RentalContentViewModel>();
    }

    protected override void RegisterDependencies(IContainer container)
    {
        container.RegisterSingleton<IBicycleSharingService, BicycleSharingService>();
        container.RegisterSingleton<IRentalOfficeService, RentalOfficeService>();
        container.RegisterSingleton<IMenuManager, MenuManager>();

        container.RegisterSingleton<IView, MainContent>();
        container.RegisterSingleton<IView, MenuContent>();
        container.RegisterSingleton<IView, BicycleContent>("BicycleContent");
        container.RegisterSingleton<IView, RentalContent>("RentalContent");
    }

    protected override void LayerInitializer(IContainer container, ILayerManager layer)
    {
        IView mainContent = container.Resolve<MainContent>();
        IView menuContent = container.Resolve<MenuContent>();

        layer.Mapping("MainLayer", mainContent);
        layer.Mapping("MenuLayer", menuContent);
    }

    protected override void OnStartup()
    {
    }
}
