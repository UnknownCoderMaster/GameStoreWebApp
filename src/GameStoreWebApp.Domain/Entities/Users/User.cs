using GameStoreWebApp.Domain.Commons;
using GameStoreWebApp.Domain.Enums;

namespace GameStoreWebApp.Domain.Entities.Users;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public int CountryId { get; set; }
    public int RegionId { get; set; }
    public UserRole Role { get; set; }
    public string ImagePath { get; set; }
}
