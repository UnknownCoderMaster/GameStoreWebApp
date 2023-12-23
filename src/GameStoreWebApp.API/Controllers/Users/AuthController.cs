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
	/// <returns></returns>
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

	[HttpGet("verify-email")]
	public async ValueTask<IActionResult> VerifyEmail([FromQuery] VerificationForCreationDto dto)
		=> Ok(await authService.ConfirmEmail(dto));
}
