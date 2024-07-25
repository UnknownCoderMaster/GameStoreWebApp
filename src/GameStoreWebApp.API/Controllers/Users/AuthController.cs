using GameStoreWebApp.Service.DTOs.Users;
using GameStoreWebApp.Service.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameStoreWebApp.API.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IAuthService authService;
	private readonly IUserService userService;
	public AuthController(IAuthService authService, IUserService userService)
	{
		this.authService = authService;
		this.userService = userService;
	}

	/// <summary>
	/// Authorization
	/// </summary>
	/// <param name="dto"></param>
	/// <returns>This Endpoint returns new token</returns>
	/// <remarks>
	/// Sample request:
	///
	///     POST /Auth
	///     {
	///        "email": "example@mail.ru",
	///        "password": Ex@mpl3
	///     }
	///
	/// </remarks>
	/// <response code="201">Returns the newly created item</response>
	/// <response code="400">If the item is null</response>
	[HttpPost("login")]
	public async ValueTask<IActionResult> Login(UserForLoginDTO dto)
	{
		var token = await authService.GenerateToken(dto.Email, dto.Password);
		return Ok(new
		{
			token
		});
	}

	/// <summary>
	/// Register new user
	/// </summary>
	/// <param name="userCreateDto"></param>
	/// <returns></returns>
	[HttpPost("register")]
	public async ValueTask<IActionResult> CreateAsync(UserCreateDto userCreateDto)
	=> Ok(await userService.CreateAsync(userCreateDto));

	/// <summary>
	/// Verify user's email
	/// </summary>
	/// <param name="dto"></param>
	/// <returns></returns>
	[HttpGet("verify-email")]
	public async ValueTask<IActionResult> VerifyEmail([FromQuery] VerificationForCreationDto dto)
		=> Ok(await authService.ConfirmEmail(dto));
}
