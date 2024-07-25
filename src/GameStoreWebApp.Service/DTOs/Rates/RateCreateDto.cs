using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameStoreWebApp.Service.DTOs.Rates;

public record RateCreateDto(
    [Required]
    int Stars, 
    [DefaultValue("Comment for game")]
    string Message, 
    int GameId, 
    int UserId
);