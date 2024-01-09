using GameStoreWebApp.Domain.Enums;

using System;
namespace GameStoreWebApp.Service.DTOs.Games;

public record GameGetDto(int Id, string Name, string Description, Category Category, DateOnly ReleaseDate, string Developer, int Downloads);
