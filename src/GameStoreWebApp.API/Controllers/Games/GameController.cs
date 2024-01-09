using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Service.DTOs.Games;
using GameStoreWebApp.Service.Interfaces.Games;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameStoreWebApp.API.Controllers.Games;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
	private readonly IGameService gameService;

	public GameController(IGameService gameService)
    {
		this.gameService = gameService;
	}

	[HttpPost]
	public async ValueTask<IActionResult> CreateGameAsync([FromForm] GameCreateDto gameCreateDto)
		=> Ok(await gameService.CreateAsync(gameCreateDto));

	[HttpPut("{id}")]
	public async ValueTask<IActionResult> UpdateGameAsync([FromRoute] int id, GameUpdateDto gameUpdateDto)
		=> Ok(await gameService.UpdateAsync(id, gameUpdateDto));

	[HttpDelete("{id}")]
	public async ValueTask<IActionResult> DeleteGameAsync([FromRoute] int id)
		=> Ok(await gameService.DeleteAsync(id));

	[HttpGet("{id}")]
	public async ValueTask<IActionResult> GetGameAsync([FromRoute] int id)
		=> Ok(await gameService.GetAsync(g => g.Id == id));

	[HttpGet]
	public async ValueTask<IActionResult> GetAllGamesAsync([FromQuery] PaginationParams @params)
		=> Ok(await gameService.GetAllAsync(@params));
}
