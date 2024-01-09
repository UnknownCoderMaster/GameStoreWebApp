using System.ComponentModel.DataAnnotations;

namespace GameStoreWebApp.Service.DTOs.Rates;

public record RateCreateDto(int Stars, string Message, int GameId, int UserId);