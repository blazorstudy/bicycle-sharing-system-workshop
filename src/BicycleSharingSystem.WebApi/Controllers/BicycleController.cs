using BicycleSharingSystem.WebApi.Contexts;
using BicycleSharingSystem.WebApi.Models;

using Microsoft.AspNetCore.Mvc;

namespace BicycleSharingSystem.WebApi.Controllers;

/// <summary>
/// Bicycle Sharing Controller
/// </summary>
[ApiController]
[Route("[controller]")]
public sealed class BicycleController(BicycleSharingContext context) : ControllerBase
{
    /// <summary>
    /// HTTP GET
    /// </summary>
    /// <returns>Bicycle data</returns>
    [HttpGet("{id:guid}")]
    public BicycleModel? Get(Guid id)
    {
        return context.Bicycles.FirstOrDefault(b => b.BicycleId == id);
    }

    /// <summary>
    /// HTTP POST
    /// </summary>
    /// <param name="bicycle">bicycle</param>
    /// <returns>Result</returns>
    [HttpPost]
    public async Task<IActionResult> Post(BicycleModel bicycle)
    {
        if (context.Bicycles.Any(x => x.BicycleId == bicycle.BicycleId))
        {
            return BadRequest($"\"{bicycle.BicycleId}\" is already exist.");
        }

        context.Bicycles.Add(bicycle);

        return await context.SaveChangesAsync().ConfigureAwait(false) > 0
            ? Accepted()
            : StatusCode(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// HTTP PUT
    /// </summary>
    /// <param name="id">bicycle id</param>
    /// <param name="bicycle">bicycle</param>
    /// <returns>Result</returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, BicycleModel bicycle)
    {
        var dbBicycle = context.Bicycles.FirstOrDefault(x => x.BicycleId == bicycle.BicycleId);

        if (dbBicycle is null)
        {
            return NotFound(bicycle);
        }

        dbBicycle.IsRental = bicycle.IsRental;
        dbBicycle.RentalOfficeName = bicycle.RentalOfficeName;

        return await context.SaveChangesAsync().ConfigureAwait(false) > 0
            ? Accepted()
            : StatusCode(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// HTTP DELETE
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>Result</returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var dbBicycle = context.Bicycles.FirstOrDefault(x => x.BicycleId == id);

        if (dbBicycle is null)
        {
            return NotFound(id);
        }

        context.Bicycles.Remove(dbBicycle);

        return await context.SaveChangesAsync().ConfigureAwait(false) > 0
            ? Accepted()
            : StatusCode(StatusCodes.Status500InternalServerError);
    }
}