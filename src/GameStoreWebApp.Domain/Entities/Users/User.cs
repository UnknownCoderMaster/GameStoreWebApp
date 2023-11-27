using GameStoreWebApp.Domain.Commons;
using GameStoreWebApp.Domain.Entities.Addresses;
using GameStoreWebApp.Domain.Entities.Feedbacks;
using GameStoreWebApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GameStoreWebApp.Domain.Entities.Users;

public class User : BaseEntity
{
    [MaxLength(30)]
    public string FirstName { get; set; }
	[MaxLength(60)]
	public string LastName { get; set; }
    [MaxLength(60)]
    [EmailAddress]
    public string Email { get; set; }
	[MaxLength(16), Phone]
	public string PhoneNumber { get; set; }
    [MaxLength(60)]
    public string Password { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public int RegionId { get; set; }
    public Region Region { get; set; }
    public UserRole Role { get; set; }
    public string? ImagePath { get; set; }
    public List<Rate> Rates { get; set; }
    public List<Feedback> Feedbacks { get; set; }
}
