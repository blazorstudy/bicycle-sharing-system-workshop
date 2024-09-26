using BicycleSharingSystem.Support.Local.Models;

namespace BicycleSharingSystem.Support.Local.Services;

public interface IBicycleSharingService
{
    Task<BicycleModel?> GetBicycleAsync(Guid id);
    Task<int> AddBicyclesAsync(IEnumerable<BicycleModel> bicycles);
    Task<bool> UpdateBicycleAsync(Guid id, BicycleModel bicycle);
    Task<bool> DeleteBicycleAsync(Guid id);
}
