using BicycleSharingSystem.MAUI.Pages;
using BicycleSharingSystem.MAUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleSharingSystem.MAUI.ViewModels
{
    public partial  class MainPageViewModel : ViewModelBase
    {
        IAPIService apiService;

        [ObservableProperty]
        public int currentTabIdx = 0;

        [ObservableProperty]
        public View currentPage;


        public MainPageViewModel()
        {
            apiService = Services.AppServiceProvider.GetService<IAPIService>();


            Task.Run(async () =>
            {
                await InitAsync();
                changeTab("0");
                changeTab("1");
                changeTab("0");
            });
            
        }
        public async Task InitAsync()
        {
            await apiService.GetRentalOfficesAsync();
        }

        [RelayCommand]
        public void changeTab(string idx)
        {
            CurrentTabIdx = Convert.ToInt32(idx);
            CurrentPage = CurrentTabIdx switch
            {
                0 => new TabSearchPage(),
                1 => new TabResvPage(),
                _ => CurrentPage
            };
        }
    }
}
