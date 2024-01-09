using GameStoreWebApp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;

namespace GameStoreWebApp.Service.DTOs.Games;

public record GameCreateDto(
	string Name, 
	string Description,
	Category Category, 
	DateOnly ReleaseDate, 
	string Developer, 
	IFormFile File);
