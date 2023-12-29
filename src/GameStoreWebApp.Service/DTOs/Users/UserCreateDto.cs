using System.ComponentModel.DataAnnotations;

namespace GameStoreWebApp.Service.DTOs.Users;

public class UserCreateDto
{
    public string FirstName { get; set; }
    public string Lastname { get; set; }
    public int CountryId { get; set; }
    public int RegionId { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    [MinLength(8)]
    public string Password { get; set; }
}
