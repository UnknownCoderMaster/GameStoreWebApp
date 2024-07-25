using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameStoreWebApp.Service.DTOs.Users;

// public record UserForLoginDTO(
//     [DefaultValue("example@mail.ru")]
//     string Email, 
//     [DefaultValue("P@$$w0rd")]
//     [MinLength(8)]
//     string Password
// );

public class UserForLoginDTO{
    [DefaultValue("example@mail.ru")]
    [Required]
    public string Email { get; set; }
    [DefaultValue("P@$$w0rd")]
    public string Password { get; set; }
}