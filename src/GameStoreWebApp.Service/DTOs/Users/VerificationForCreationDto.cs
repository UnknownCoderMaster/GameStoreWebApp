using System;
using System.ComponentModel.DataAnnotations;

namespace GameStoreWebApp.Service.DTOs.Users;

public class VerificationForCreationDto
{
	[Required]
	public Guid Code { get; set; }
	[Required]
	public string Email { get; set; }
}