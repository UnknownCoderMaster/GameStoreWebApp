namespace GameStoreWebApp.Service.DTOs.Users;

public record UserGetDto(int Id, string FirstName, string LastName, string Email, string CountryName, string RegionName);