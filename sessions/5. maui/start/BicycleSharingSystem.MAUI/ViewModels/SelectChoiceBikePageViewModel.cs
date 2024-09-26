using BicycleSharingSystem.MAUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleSharingSystem.MAUI.ViewModels
{
    public partial class SelectChoiceBikePageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private Models.BicycleModel selectedBicycle = new Models.BicycleModel();

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private string paramName;

        [ObservableProperty]
        private Models.RentalOfficeModel currentOfficeItem = new Models.RentalOfficeModel();

        private readonly IAPIService apiService;

        public SelectChoiceBikePageViewModel()
        {
            apiService = Services.AppServiceProvider.GetService<IAPIService>();
        }

        [RelayCommand]
        public async Task SelectedBikeChange(Models.BicycleModel selectedItem)
        {
            if (selectedItem == null)
                return;

            SelectedBicycle = selectedItem;
        }

        [RelayCommand]
        public async Task MoveNextPage()
        {
            if (IsLoading) return; // 로딩 중일 때는 클릭 제한

            if (SelectedBicycle.IsRented)
            {
                await App.Current.MainPage.DisplayAlert("알림", "이미 대여 중인 자전거입니다", "확인");
            }
            else
            {
                await Shell.Current.GoToAsync($"./SelectChoiceFeePage?bicycleid={SelectedBicycle.BicycleId}&officeid={CurrentOfficeItem.OfficeId}", false);
            }
        }

        [RelayCommand]
        public async Task BackPage()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async Task InitAsync()
        {
            IsLoading = true;

            try
            {
                var detailData = await apiService.GetCurrentRentalOfficeAsync(paramName);
                if (detailData != null)
                {
                    CurrentOfficeItem = detailData;

                    if (CurrentOfficeItem.Bicycles != null && CurrentOfficeItem.Bicycles.Any())
                    {
                        await SelectedBikeChange(CurrentOfficeItem.Bicycles.First());
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("알림", "대여소 정보를 불러오지 못했습니다.", "확인");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("알림", ex.Message, "확인");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
