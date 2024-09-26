using System.Net.Http.Json;
using BicycleSharingSystem.Support.Local.Models;

namespace BicycleSharingSystem.Support.Local.Services;

public class BicycleSharingService : IBicycleSharingService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://localhost:7178";

    public BicycleSharingService()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };
    }

    public async Task<BicycleModel?> GetBicycleAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<BicycleModel>($"/Bicycle/{id}");
    }

    public async Task<int> AddBicyclesAsync(IEnumerable<BicycleModel> bicycles)
    {
        var response = await _httpClient.PostAsJsonAsync("/Bicycle", bicycles);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task<bool> UpdateBicycleAsync(Guid id, BicycleModel bicycle)
    {
        var response = await _httpClient.PutAsJsonAsync($"/Bicycle/{id}", bicycle);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteBicycleAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"/Bicycle/{id}");
        return response.IsSuccessStatusCode;
    }
}
