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
    /// <param name="bicycles">bicycles</param>
    /// <returns>Result</returns>
    [HttpPost]
    public async Task<IActionResult> Post(IEnumerable<BicycleModel> bicycles)
    {
        try
        {
            context.Bicycles.AddRange(bicycles);

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
    /// <param name="id">bicycle id</param>
    /// <param name="bicycle">bicycle</param>
    /// <returns>Result</returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, BicycleModel bicycle)
    {
        var dbBicycle = context.Bicycles.FirstOrDefault(x => x.BicycleId == id);

        if (dbBicycle is null)
        {
            return NotFound(bicycle);
        }

        try
        {
            context.Bicycles.Remove(dbBicycle);
            context.Bicycles.Add(bicycle);

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
        var dbBicycle = context.Bicycles.FirstOrDefault(x => x.BicycleId == id);

        if (dbBicycle is null)
        {
            return NotFound(id);
        }

        try
        {
            context.Bicycles.Remove(dbBicycle);

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