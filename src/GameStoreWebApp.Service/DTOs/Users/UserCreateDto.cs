using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameStoreWebApp.Service.DTOs.Users;

public class UserCreateDto
{
    [DefaultValue("John")]
    public string FirstName { get; set; }
    [DefaultValue("Doe")]
    public string Lastname { get; set; }
    [DefaultValue(1)]
    public int CountryId { get; set; }
    [DefaultValue(1)]
    public int RegionId { get; set; }
    [DefaultValue("example@mail.ru")]
    public string Email { get; set; }
    [DefaultValue("+998901234567")]
    public string PhoneNumber { get; set; }
    [MinLength(8)]
    [DefaultValue("P@$$w0rd")]
    public string Password { get; set; }
}
