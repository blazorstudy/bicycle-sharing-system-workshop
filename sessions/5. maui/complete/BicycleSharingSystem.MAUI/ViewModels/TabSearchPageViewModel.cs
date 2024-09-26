using BicycleSharingSystem.MAUI.Messegers;
using BicycleSharingSystem.MAUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Maps;
using System.Collections.ObjectModel;

namespace BicycleSharingSystem.MAUI.ViewModels
{
    public partial class TabSearchPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        public bool isLoading;

        [ObservableProperty]
        public ObservableCollection<Models.RentalOfficeModel> rentalOffices = new ObservableCollection<Models.RentalOfficeModel>();

        [ObservableProperty]
        public Models.RentalOfficeModel currentOfficeItem = new Models.RentalOfficeModel();

        IAPIService apiService;

        public TabSearchPageViewModel()
        {            
            apiService = Services.AppServiceProvider.GetService<IAPIService>();
            WeakReferenceMessenger.Default.Register<RefreshExploreTabMessage>(this, async (r, message) =>
            {
                await apiService.GetRentalOfficesAsync();
                var dd=  apiService.GetSaveRentalOffices();
                await InitAsync();
            });
            // ViewModel 생성 시 비동기 초기화 호출
            Task.Run(async () => await InitAsync());
        }

        [RelayCommand]
        public void SelectedBikeChange(Models.RentalOfficeModel selectedItem)
        {
            CurrentOfficeItem = selectedItem;
            WeakReferenceMessenger.Default.Send(new MapMoveToLocationMessage(selectedItem.OfficeLocation));
        }

        [RelayCommand]
        public async Task SelectItemMove()
        {
            await Shell.Current.GoToAsync($"./SelectChoiceBikePage?name={currentOfficeItem.Name}", false);
        }

        public async Task InitAsync()
        {
            IsLoading = true;
            try
            {
                foreach (var item in apiService.GetSaveRentalOffices())
                {
                    var detailData = await apiService.GetCurrentRentalOfficeAsync(item.Name);
                    detailData.Latitude = item.Latitude;
                    detailData.Longitude = item.Longitude;

                    var currentItem = RentalOffices.Where(_ => _.OfficeId == item.OfficeId).FirstOrDefault();
                    if (currentItem != null)
                    {
                        // 기존 항목을 교체
                        var index = RentalOffices.IndexOf(currentItem);
                        RentalOffices[index] = detailData;
                    }
                    else
                    {
                        // 새로운 항목 추가
                        RentalOffices.Add(detailData);
                    }
                }


                // 지도 위치를 첫 번째 대여소로 이동하는 메시지 전송
                if (RentalOffices.Any())
                {
                    CurrentOfficeItem = RentalOffices[0];
                    var firstOfficeLocation = RentalOffices[0].OfficeLocation;
                    if (firstOfficeLocation != null)
                    {// 메인 스레드에서 지도 업데이트를 실행

                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            WeakReferenceMessenger.Default.Send(new MapMoveToLocationMessage(firstOfficeLocation));
                        });
                    }
                }

                //var result = await apiService.GetRentalOfficesAsync();
                // 비동기 초기화 로직 작성
            }
            catch(Exception ex)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
