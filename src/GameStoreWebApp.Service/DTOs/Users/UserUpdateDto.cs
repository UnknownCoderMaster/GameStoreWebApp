using System.ComponentModel.DataAnnotations;

namespace GameStoreWebApp.Service.DTOs.Users;

public record UserUpdateDto([Required] string Email);