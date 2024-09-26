using BicycleSharingSystem.MAUI.Messegers;
using BicycleSharingSystem.MAUI.Models;
using BicycleSharingSystem.MAUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Plugin.LocalNotification;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleSharingSystem.MAUI.ViewModels
{
    public partial class TabResvPageViewModel : ViewModels.ViewModelBase
    {
        private readonly IAPIService apiService;

        [ObservableProperty]
        public bool isItemEmpty = true;

        [ObservableProperty]
        public bool isItemExist = false;

        [ObservableProperty]
        private RentalOfficeModel currentOfficeData;

        [ObservableProperty]
        private BicycleModel currentBycleData;

        private int returnNotificationId = 1337; // 고유한 알림 ID 설정

        public TabResvPageViewModel(IAPIService apiService)
        {
            this.apiService = apiService;
            WeakReferenceMessenger.Default.Register<Messegers.RefreshRentalStatusMessage>(this, (r, message) =>
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await this.RefreshRentalStatus(message.BicycleId, message.OfficeId);
                });
            });
        }

        public async Task RefreshRentalStatus(string bicycleId, string officeId)
        {
            var officeData = apiService.GetSaveRentalOffices().SingleOrDefault(_ => _.OfficeId == officeId);
            var bicycleData = await apiService.GetBicycleAsync(bicycleId);
            
            MainThread.BeginInvokeOnMainThread(() =>
            {
                CurrentOfficeData = officeData;
                CurrentBycleData = bicycleData;
                IsItemEmpty = CurrentBycleData == null || CurrentOfficeData == null;
                IsItemExist = CurrentBycleData != null && CurrentOfficeData != null;                
            });
            ScheduleBicycleReturnNotification();

        }

        [RelayCommand]
        public async Task ReturnBicycle()
        {
            if (CurrentBycleData == null || CurrentOfficeData == null)
            {
                await Application.Current.MainPage.DisplayAlert("에러", "반납할 자전거나 대여소 정보가 없습니다.", "확인");
                return;
            }
            // 바텀 시트 또는 팝업을 띄워 대여소 선택
            string selectedOfficeId = await ShowReturnOfficeBottomSheet(apiService.GetSaveRentalOffices());
            if (!string.IsNullOrEmpty(selectedOfficeId))
            {
                bool isSuccess = await apiService.ReturnBicycleAsync(CurrentBycleData.BicycleId, CurrentOfficeData.OfficeId);

                if (isSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "자전거를 성공적으로 반납하였습니다.", "확인");
                    // 예약된 알림 취소
                    LocalNotificationCenter.Current.Cancel(returnNotificationId);
                    // 상태 초기화
                    CurrentOfficeData = null;
                    CurrentBycleData = null;
                    

                    // 상태 갱신
                    IsItemEmpty = true;
                    IsItemExist = false;

                    // 반납 후 상태 갱신을 위한 메시지 전송 (필요할 경우)
                    WeakReferenceMessenger.Default.Send(new RefreshExploreTabMessage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("에러", "자전거 반납에 실패하였습니다. 다시 시도해주세요.", "확인");
                }
            }
        }
        private async Task<string> ShowReturnOfficeBottomSheet(List<RentalOfficeModel> rentalOffices)
        {
            //사용자가 대여소를 선택하는 바텀 시트를 띄우고 결과 반환
            var officeNames = rentalOffices.Select(office => office.Name).ToArray();
            string selectedOffice = await Application.Current.MainPage.DisplayActionSheet("반납할 대여소를 선택하세요", "취소", null, officeNames);

            var selectedOfficeModel = rentalOffices.FirstOrDefault(office => office.Name == selectedOffice);

            return selectedOfficeModel?.OfficeId;
        }

        public async void ScheduleBicycleReturnNotification()
        {
            int notifySecond = 30;
            if (CurrentBycleData != null && CurrentBycleData.ExpireRentalTime != null)
            {
                if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
                {
                    await LocalNotificationCenter.Current.RequestNotificationPermission();
                }
                // ExpireRentalTime을 UTC로 변환하여 알림 시간을 설정
                DateTime notifyTime = CurrentBycleData.ExpireRentalTime.Value.AddSeconds(notifySecond*-1);

                var request = new NotificationRequest
                {
                    NotificationId = returnNotificationId, // 고유 알림 ID
                    Title = "자전거 반납 요청 안내",
                    Subtitle = "BMW",
                    Description = $"반납 시간이 {notifySecond}초 남았습니다.",
                    BadgeNumber = 1,
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = notifyTime, 
                        NotifyRepeatInterval = null, // 반복하지 않음
                    }
                };

                // 알림 등록
                await LocalNotificationCenter.Current.Show(request);
            }
            else
            {
                Console.WriteLine("CurrentBycleData 또는 ExpireRentalTime이 유효하지 않습니다.");
            }
        }
    }
}
