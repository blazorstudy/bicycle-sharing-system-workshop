

using BicycleSharingSystem.MAUI.ViewModels;

namespace BicycleSharingSystem.MAUI.Pages;

[QueryProperty(nameof(Name), "name")]
public partial class SelectChoiceBikePage : ContentPage
{
    public string Name { get; set; }

    public SelectChoiceBikePage()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm = (this.BindingContext as SelectChoiceBikePageViewModel);
        vm.ParamName = Name;
        await vm.InitAsync();
    }
}