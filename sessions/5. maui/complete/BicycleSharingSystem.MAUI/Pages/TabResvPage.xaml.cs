using BicycleSharingSystem.MAUI.ViewModels;
using CommunityToolkit.Mvvm.Messaging;

namespace BicycleSharingSystem.MAUI.Pages;

public partial class TabResvPage : ContentView
{
	public TabResvPage()
	{
		InitializeComponent();
        this.BindingContext = Services.AppServiceProvider.GetService<TabResvPageViewModel>();
    }
}           