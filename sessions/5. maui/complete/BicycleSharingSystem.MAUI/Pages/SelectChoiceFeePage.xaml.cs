using BicycleSharingSystem.MAUI.ViewModels;

namespace BicycleSharingSystem.MAUI.Pages;

[QueryProperty(nameof(BicycleId), "bicycleid")]
[QueryProperty(nameof(Officeid), "officeid")]
public partial class SelectChoiceFeePage : ContentPage
{
    public string BicycleId { get; set; }

    public string Officeid { get; set; }

    public SelectChoiceFeePage()
	{
		InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm = (this.BindingContext as SelectChoiceFeePageViewModel);
        vm.ParamBicycleId = BicycleId;
        vm.ParamOfficeId = Officeid;
    }
}