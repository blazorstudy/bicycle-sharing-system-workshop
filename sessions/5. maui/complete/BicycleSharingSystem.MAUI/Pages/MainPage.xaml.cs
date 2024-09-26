
using BicycleSharingSystem.MAUI.ViewModels;

namespace BicycleSharingSystem.MAUI.Pages
{
    [QueryProperty(nameof(ParamIdx), "paramidx")]
    public partial class MainPage : ContentPage
    {
        public string ParamIdx { get; set; }
        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!String.IsNullOrEmpty(ParamIdx))
            {
                var vm = (this.BindingContext as MainPageViewModel);
                await vm.InitAsync();
                vm.changeTab(ParamIdx);
                ParamIdx = "0";
            }
        }
    }
}
