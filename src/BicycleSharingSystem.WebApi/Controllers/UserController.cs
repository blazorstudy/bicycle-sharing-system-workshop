using BicycleSharingSystem.WebApi.Contexts;

using Microsoft.AspNetCore.Mvc;

namespace BicycleSharingSystem.WebApi.Controllers;

/// <summary>
/// Rental Controller.
/// </summary>
[ApiController]
[Route("[controller]")]
public sealed class UserController(BicycleSharingContext context) : ControllerBase
{
    /// <summary>
    /// Rental
    /// </summary>
    /// <param name="id">bicycle id</param>
    /// <returns>Result</returns>
    [HttpPut("Rental/{id:guid}")]
    public Task<IActionResult> RentalAsync(Guid id) => ProcessAsync(id, true);

    /// <summary>
    /// Return
    /// </summary>
    /// <param name="id">bicycle id</param>
    /// <returns>Result</returns>
    [HttpPut("Return/{id:guid}")]
    public Task<IActionResult> ReturnAsync(Guid id) => ProcessAsync(id, false);

    private async Task<IActionResult> ProcessAsync(Guid id, bool isRental)
    {
        var dbBicycle = context.Bicycles.FirstOrDefault(x => x.BicycleId == id);

        if (dbBicycle is null)
        {
            return BadRequest();
        }

        dbBicycle.IsRental = isRental;

        return await context.SaveChangesAsync().ConfigureAwait(false) > 0
            ? Accepted()
            : StatusCode(StatusCodes.Status500InternalServerError);
    }
}