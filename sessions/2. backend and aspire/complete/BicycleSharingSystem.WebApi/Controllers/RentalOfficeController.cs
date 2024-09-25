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
    public object? Get(string name)
    {
        var rentalOffice = context.RentalOffices.FirstOrDefault(o => o.Name == name);

        if (rentalOffice == null)
        {
            return default;
        }

        return new
        {
            OfficeId = rentalOffice.OfficeId,
            Name = rentalOffice.Name,
            Region = rentalOffice.Region,
            Latitude= rentalOffice.Latitude,
            Longitude = rentalOffice.Longitude,
            Bicycles = context.Bicycles.Where(x => x.RentalOfficeId == rentalOffice.OfficeId)
        };
    }

    /// <summary>
    /// HTTP POST
    /// </summary>
    /// <param name="rentalOffices">rentalOffices</param>
    /// <returns>Result</returns>
    [HttpPost]
    public async Task<IActionResult> Post(IEnumerable<RentalOfficeModel> rentalOffices)
    {
        try
        {
            context.RentalOffices.AddRange(rentalOffices);

            var changes = await context.SaveChangesAsync().ConfigureAwait(false);

            return Ok(changes);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// HTTP PUT
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="updateRentalOffice">updateRentalOffice</param>
    /// <returns>Result</returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, RentalOfficeModel updateRentalOffice)
    {
        var rentalOffice = context.RentalOffices.FirstOrDefault(o => o.OfficeId == id);

        if (rentalOffice is null)
        {
            return NotFound($"\"{id}\" cannot be found.");
        }

        try
        {
            context.RentalOffices.Remove(rentalOffice);
            context.RentalOffices.Add(updateRentalOffice);

            return await context.SaveChangesAsync().ConfigureAwait(false) > 0
                ? Accepted()
                : StatusCode(StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// HTTP DELETE
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>Result</returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var previousOffice = context.RentalOffices.FirstOrDefault(o => o.OfficeId == id);

        if (previousOffice is null)
        {
            return NotFound($"\"{name}\" cannot be found.");
        }

        try
        {
            context.RentalOffices.Remove(previousOffice);

            return await context.SaveChangesAsync().ConfigureAwait(false) > 0
                ? Accepted()
                : StatusCode(StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}