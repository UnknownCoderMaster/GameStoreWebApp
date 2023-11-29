using System;

namespace GameStoreWebApp.Service.DTOs.Users;

public class VerificationForCreationDto
{
	public Guid Code { get; set; }
	public string Email { get; set; }
}
