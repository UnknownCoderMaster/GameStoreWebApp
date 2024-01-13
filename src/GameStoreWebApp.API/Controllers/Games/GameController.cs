using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Service.DTOs.Games;
using GameStoreWebApp.Service.Interfaces.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameStoreWebApp.API.Controllers.Games;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GameController : ControllerBase
{
	private readonly IGameService gameService;

	public GameController(IGameService gameService)
    {
		this.gameService = gameService;
	}

	/// <summary>
	/// Add new Game
	/// </summary>
	/// <param name="gameCreateDto"></param>
	/// <returns></returns>
	[HttpPost, Authorize(Roles = "Admin")]
	public async ValueTask<IActionResult> CreateGameAsync([FromForm] GameCreateDto gameCreateDto)
		=> Ok(await gameService.CreateAsync(gameCreateDto));

	/// <summary>
	/// Update game's informations and files
	/// </summary>
	/// <param name="id"></param>
	/// <param name="gameUpdateDto"></param>
	/// <returns></returns>
	[HttpPut("{id}"), Authorize(Roles = "Admin")]
	public async ValueTask<IActionResult> UpdateGameAsync([FromRoute] int id, [FromForm] GameUpdateDto gameUpdateDto)
		=> Ok(await gameService.UpdateAsync(id, gameUpdateDto));

	/// <summary>
	/// Delete all data and files related to the game
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpDelete("{id}"), Authorize(Roles = "Admin")]
	public async ValueTask<IActionResult> DeleteGameAsync([FromRoute] int id)
		=> Ok(await gameService.DeleteAsync(id));

	/// <summary>
	/// Get all game data by Id
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpGet("{id}")]
	public async ValueTask<IActionResult> GetGameAsync([FromRoute] int id)
		=> Ok(await gameService.GetAsync(g => g.Id == id));

	/// <summary>
	/// Get information about all games
	/// </summary>
	/// <param name="params"></param>
	/// <returns></returns>
	[HttpGet]
	public async ValueTask<IActionResult> GetAllGamesAsync([FromQuery] PaginationParams @params)
		=> Ok(await gameService.GetAllAsync(@params));
}
