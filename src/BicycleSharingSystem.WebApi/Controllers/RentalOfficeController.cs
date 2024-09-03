using BicycleSharingSystem.WebApi.Contexts;
using BicycleSharingSystem.WebApi.Models;

using Microsoft.AspNetCore.Mvc;

namespace BicycleSharingSystem.WebApi.Controllers;

/// <summary>
/// Rental Office Controller
/// </summary>
[ApiController]
[Route("[controller]")]
public sealed class RentalOfficeController(BicycleSharingContext context) : ControllerBase
{
    /// <summary>
    /// HTTP GET
    /// </summary>
    /// <returns>Rental office data</returns>
    [HttpGet]
    public IEnumerable<RentalOfficeModel> GetAll()
    {
        return context.RentalOffices;
    }

    /// <summary>
    /// HTTP GET
    /// </summary>
    /// <returns>Bicycle data</returns>
    [HttpGet("{name}")]
    public IEnumerable<BicycleModel> Get(string name)
    {
        var rentalOffice = context.RentalOffices.FirstOrDefault(o => o.Name == name);

        if (rentalOffice == null)
        {
            return [];
        }

        return context.Bicycles.Where(x => x.RentalOfficeName == rentalOffice.Name);
    }

    /// <summary>
    /// HTTP POST
    /// </summary>
    /// <param name="rentalOffice">rentalOffice</param>
    /// <returns>Result</returns>
    [HttpPost]
    public async Task<IActionResult> Post(RentalOfficeModel rentalOffice)
    {
        if (context.RentalOffices.Any(o => o.Name == rentalOffice.Name))
        {
            return BadRequest($"\"{rentalOffice.Name}\" is already exist.");
        }

        context.RentalOffices.Add(rentalOffice);

        return await context.SaveChangesAsync().ConfigureAwait(false) > 0
            ? Accepted()
            : StatusCode(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// HTTP PUT
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="updateRentalOffice">updateRentalOffice</param>
    /// <returns>Result</returns>
    [HttpPut("{name}")]
    public async Task<IActionResult> Put(string name, RentalOfficeModel updateRentalOffice)
    {
        var rentalOffice = context.RentalOffices.FirstOrDefault(o => o.Name == name);

        if (rentalOffice is null)
        {
            return NotFound($"\"{name}\" cannot be found.");
        }

        context.RentalOffices.Remove(rentalOffice);
        context.RentalOffices.Add(updateRentalOffice);

        return await context.SaveChangesAsync().ConfigureAwait(false) > 0
            ? Accepted()
            : StatusCode(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// HTTP DELETE
    /// </summary>
    /// <param name="name">name</param>
    /// <returns>Result</returns>
    [HttpDelete("{name}")]
    public async Task<IActionResult> Delete(string name)
    {
        var previousOffice = context.RentalOffices.FirstOrDefault(o => o.Name == name);

        if (previousOffice is null)
        {
            return NotFound($"\"{name}\" cannot be found.");
        }

        context.RentalOffices.Remove(previousOffice);

        return await context.SaveChangesAsync().ConfigureAwait(false) > 0
            ? Accepted()
            : StatusCode(StatusCodes.Status500InternalServerError);
    }
}