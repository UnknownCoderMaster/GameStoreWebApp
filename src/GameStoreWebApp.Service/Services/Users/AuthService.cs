using GameStoreWebApp.Service.DTOs.Users;
using GameStoreWebApp.Service.Interfaces.Users;
using System.Threading.Tasks;

namespace GameStoreWebApp.Service.Services.Users;

public class AuthService : IAuthService
{
	public ValueTask<bool> ConfirmEmail(VerificationForCreationDto dto)
	{
		throw new System.NotImplementedException();
	}

	public ValueTask<string> GenerateToken(string email, string password)
	{
		throw new System.NotImplementedException();
	}

	public ValueTask<bool> SendEmailVerification(int userId)
	{
		throw new System.NotImplementedException();
	}
}
