using BicycleSharingSystem.MAUI.Messegers;
using BicycleSharingSystem.MAUI.Models;
using BicycleSharingSystem.MAUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BicycleSharingSystem.MAUI.ViewModels
{
    public partial class SelectChoiceFeePageViewModel : ViewModelBase
    {
        [ObservableProperty]
        public List<PricingPlanModel> pricingPlans;

        [ObservableProperty]
        public PricingPlanModel selectedPlanModel = new PricingPlanModel();

        [ObservableProperty]
        public string paramBicycleId;

        [ObservableProperty]
        public string paramOfficeId;

        IAPIService apiService;

        [ObservableProperty]
        public string mostPopluarText = "가장 많은 BMW회원들이 선택하는 요금제입니다";

        public SelectChoiceFeePageViewModel()
        {
            apiService = Services.AppServiceProvider.GetService<IAPIService>();
            PricingPlans = new List<PricingPlanModel>
            {
                new PricingPlanModel
                {
                    name = "BASIC",
                    time = 1,
                    price = 800,
                    description = "짧은 거리 이동에 적합한 요금제",
                    isMostPopular = true
                },
                new PricingPlanModel
                {
                    name = "STANDARD",
                    time = 5,
                    price = 3800,
                    description = "중간 거리에 맞는 요금제",
                    isMostPopular = false
                },
                new PricingPlanModel
                {
                    name = "PRO",
                    time = 10,
                    price = 7400,
                    description = "비교적 긴 거리 이동에 적합한 요금제",
                    isMostPopular = false
                },
                new PricingPlanModel
                {
                    name = "PREMIUM",
                    time = 30,
                    price = 14900,
                    description = "장거리 이용에 적합한 요금제",
                    isMostPopular = false
                }
            };
        }

        [RelayCommand]
        public void SelectedPlanChange(Models.PricingPlanModel selectedItem)
        {
            SelectedPlanModel = selectedItem;
        }

        [RelayCommand]
        public async Task MoveNextPage()
        {
            bool isSuccess = await apiService.RentBicycleAsync(ParamBicycleId, SelectedPlanModel.time);
            if (isSuccess)
            {
                // 메신저 메시지 전송
                WeakReferenceMessenger.Default.Send(new RefreshExploreTabMessage());
                WeakReferenceMessenger.Default.Send(new RefreshRentalStatusMessage(ParamBicycleId, ParamOfficeId));

                // MainPage로 이동
                await Shell.Current.GoToAsync($"//MainPage");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("에러", "자전거 대여에 실패하였습니다! 다시 시도해주세요", "확인");
            }
        }

        [RelayCommand]
        public async Task BackPage()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
