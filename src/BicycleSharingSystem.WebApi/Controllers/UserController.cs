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
    private const int DefaultRentalTime = 1;

    /// <summary>
    /// 자전거를 대여합니다.
    /// </summary>
    /// <param name="id">자전거 id</param>
    /// <param name="time">대여시간(분)</param>
    /// <returns>Result</returns>
    [HttpPut("Rental/{id:guid}/{time:int?}")]
    public async Task<IActionResult> RentalAsync(Guid id, int time = DefaultRentalTime)
    {
        var dbBicycle = context.Bicycles.FirstOrDefault(x => x.BicycleId == id);

        if (dbBicycle is null)
        {
            return BadRequest();
        }

        var startDateTime = DateTime.Now;
        var expireDateTime = startDateTime + TimeSpan.FromMinutes(time);

        dbBicycle.StartRentalTime = startDateTime;
        dbBicycle.ExpireRentalTime = expireDateTime;

        return await context.SaveChangesAsync().ConfigureAwait(false) > 0
            ? Accepted()
            : StatusCode(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// 자전거를 반납합니다.
    /// </summary>
    /// <param name="officeId">반납장소의 ID</param>
    /// <param name="bicycleId">자전거 ID</param>
    /// <returns>Result</returns>
    [HttpPut("Return/{officeId:guid}/{bicycleId:guid}")]
    public async Task<IActionResult> ReturnAsync(Guid officeId, Guid bicycleId)
    {
        var dbBicycle = context.Bicycles.FirstOrDefault(x => x.BicycleId == bicycleId);

        if (dbBicycle is null)
        {
            return BadRequest();
        }

        dbBicycle.RentalOfficeId = officeId;
        dbBicycle.StartRentalTime = default;
        dbBicycle.ExpireRentalTime = DateTime.Now;

        return await context.SaveChangesAsync().ConfigureAwait(false) > 0
            ? Accepted()
            : StatusCode(StatusCodes.Status500InternalServerError);
    }
}