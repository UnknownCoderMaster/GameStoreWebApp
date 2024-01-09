using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Service.DTOs.Rates;
using GameStoreWebApp.Service.Interfaces.Rates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameStoreWebApp.API.Controllers.Rates;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RateController : ControllerBase
{
    private readonly IRateService rateService;

    public RateController(IRateService rateService)
    {
        this.rateService = rateService;
    }

    /// <summary>
    /// Creating Rate
    /// </summary>
    /// <param name="rateCreateDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async ValueTask<IActionResult> CreateRateAsync(RateCreateDto rateCreateDto)
        => Ok(await rateService.CreateAsync(rateCreateDto));

    /// <summary>
    /// Update rating
    /// </summary>
    /// <param name="rateUpdateDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async ValueTask<IActionResult> UpdateRateAsync([FromRoute] int id, RateUpdateDto rateUpdateDto)
        => Ok(await rateService.UpdateAsync(id, rateUpdateDto));

    /// <summary>
    /// Delete rating
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async ValueTask<IActionResult> DeleteRateAsync([FromRoute] int id)
        => Ok(await rateService.DeleteAsync(id));

	/// <summary>
	/// Get rating by Id
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpGet("{id}")]
	public async ValueTask<IActionResult> GetRateAsync([FromRoute] int id)
        => Ok(await rateService.GetAsync(r => r.Id == id));

	/// <summary>
	/// Get ratings by UserId
	/// </summary>
	/// <param name="userId"></param>
	/// <returns></returns>
	[HttpGet("user/{userId}")]
	public async ValueTask<IActionResult> GetUserRatesAsync([FromQuery] PaginationParams @params, [FromRoute] int userId)
		=> Ok(await rateService.GetAllAsync(@params, r => r.UserId == userId));

	/// <summary>
	/// Get rating by GameId
	/// </summary>
	/// <param name="gameId"></param>
	/// <returns></returns>
	[HttpGet("game/{gameId}")]
	public async ValueTask<IActionResult> GetGameRatesAsync([FromQuery] PaginationParams @params, [FromRoute] int gameId)
		=> Ok(await rateService.GetAllAsync(@params, r => r.GameId == gameId));

	/// <summary>
	/// Get all ratings
	/// </summary>
	/// <param name="params"></param>
	/// <returns></returns>
	[HttpGet, Authorize(Roles = "Admin")]
    public async ValueTask<IActionResult> GetAllRatesAsync([FromQuery] PaginationParams @params)
        => Ok(await rateService.GetAllAsync(@params));
}
