using BicycleSharingSystem.Support.Local.Models;

namespace BicycleSharingSystem.Support.Local.Services;


public interface IRentalOfficeService
{
    Task<List<RentalOfficeModel>> GetAllRentalOfficesAsync();
    Task<RentalOfficeModel> GetRentalOfficeAsync(string name);
    Task<bool> AddRentalOfficesAsync(IEnumerable<RentalOfficeModel> rentalOffices);
    Task<bool> UpdateRentalOfficeAsync(RentalOfficeModel rentalOffice);
    Task<bool> DeleteRentalOfficeAsync(string name);
}
