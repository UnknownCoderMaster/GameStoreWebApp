using GameStoreWebApp.Service.DTOs.Users;

namespace GameStoreWebApp.Service.Interfaces.Users;

public interface IAuthService
{
	ValueTask<string> GenerateToken(string email, string password);
	ValueTask<bool> SendEmailVerification(int userId);
	ValueTask<bool> ConfirmEmail(VerificationForCreationDto dto);
}