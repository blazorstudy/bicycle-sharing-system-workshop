using System.Net.Http.Json;
using BicycleSharingSystem.Support.Local.Models;

namespace BicycleSharingSystem.Support.Local.Services;

public class RentalOfficeService : IRentalOfficeService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://localhost:7178";

    public RentalOfficeService()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };
    }

    public async Task<List<RentalOfficeModel>> GetAllRentalOfficesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<RentalOfficeModel>>("/RentalOffice") ?? [];
    }

    public async Task<RentalOfficeModel> GetRentalOfficeAsync(string name)
    {
        return await _httpClient.GetFromJsonAsync<RentalOfficeModel>($"/RentalOffice/{name}");
    }

    public async Task<bool> AddRentalOfficesAsync(IEnumerable<RentalOfficeModel> rentalOffices)
    {
        var response = await _httpClient.PostAsJsonAsync("/RentalOffice", rentalOffices);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateRentalOfficeAsync(RentalOfficeModel rentalOffice)
    {
        var response = await _httpClient.PutAsJsonAsync($"/RentalOffice/{rentalOffice.OfficeId}", rentalOffice);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteRentalOfficeAsync(string name)
    {
        var response = await _httpClient.DeleteAsync($"/RentalOffice/{name}");
        return response.IsSuccessStatusCode;
    }
}
