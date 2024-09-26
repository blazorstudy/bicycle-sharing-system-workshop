using BicycleSharingSystem.MAUI.Models;
using Newtonsoft.Json;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleSharingSystem.MAUI.Services
{
    public interface IAPIService
    {
        Task<RentalOfficeModel> GetCurrentRentalOfficeAsync(string rentalOfficeName);

        // 대여소 목록을 가져오는 메서드
        Task<List<RentalOfficeModel>> GetRentalOfficesAsync();

        // 특정 대여소의 자전거 목록을 가져오는 메서드
        Task<List<BicycleModel>> GetRentalOfficeBicyclesAsync(string rentalOfficeId);

        // 자전거 대여 메서드
        Task<bool> RentBicycleAsync(string bicycleId, int rentalTime);

        // 자전거 대여 메서드
        Task<BicycleModel> GetBicycleAsync(string bicycleId);

        // 자전거 반납 메서드
        Task<bool> ReturnBicycleAsync(string bicycleId, string rentalOfficeId);

        List<RentalOfficeModel> GetSaveRentalOffices();
    }

    public class APIService : IAPIService
    {
        private readonly HttpClient _httpClient;
        private List<RentalOfficeModel> rentalOffices;

        public APIService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://4212-116-43-38-237.ngrok-free.app")
            };
        }

        // 대여소 목록을 가져오는 메서드 구현
        public async Task<List<RentalOfficeModel>> GetRentalOfficesAsync()
        {
            var response = await _httpClient.GetAsync("/RentalOffice");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            rentalOffices = JsonConvert.DeserializeObject<List<RentalOfficeModel>>(content);
            return rentalOffices;
        }

        // 특정 대여소의 자전거 목록을 가져오는 메서드 구현
        public async Task<List<BicycleModel>> GetRentalOfficeBicyclesAsync(string rentalOfficeId)
        {
            var response = await _httpClient.GetAsync($"/RentalOffice/{rentalOfficeId}/bicycles");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<BicycleModel>>(content);
        }

        // 자전거 대여 메서드 구현
        public async Task<bool> RentBicycleAsync(string bicycleId, int rentalTime)
        {
            var response = await _httpClient.PutAsync($"/User/Rental/{bicycleId}/{rentalTime}", null);
            return response.IsSuccessStatusCode;
        }

        // 자전거 반납 메서드 구현
        public async Task<bool> ReturnBicycleAsync(string bicycleId, string rentalOfficeId)
        {
            var response = await _httpClient.PutAsync($"/User/Return/{rentalOfficeId}/{bicycleId}", null);
            return response.IsSuccessStatusCode;
        }

        //이미 저장 된 대여소 반환
        public List<RentalOfficeModel> GetSaveRentalOffices()
        {
            return rentalOffices;
        }

        //지정 대여소 현황 반환
        public async Task<RentalOfficeModel> GetCurrentRentalOfficeAsync(string rentalOfficeName)
        {
            var response = await _httpClient.GetAsync("/RentalOffice/" + rentalOfficeName);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RentalOfficeModel>(content);
        }

        //지정 자전거 정보 반환
        public async Task<BicycleModel> GetBicycleAsync(string bicycleId)
        {
            var response = await _httpClient.GetAsync("/Bicycle/" + bicycleId);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BicycleModel>(content);
        }
    }
}
