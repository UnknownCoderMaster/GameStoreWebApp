using System.ComponentModel.DataAnnotations;

namespace GameStoreWebApp.Service.DTOs.Users;

public class UserForChangePasswordDto
{
	[Required]
	public string OldPassword { get; set; }

	[Required]
	public string NewPassword { get; set; }
}
