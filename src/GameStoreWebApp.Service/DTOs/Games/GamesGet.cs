﻿using GameStoreWebApp.Domain.Enums;

namespace GameStoreWebApp.Service.DTOs.Games;

public record GameGet(int Id, string GameName, string Description, Category Category, DateOnly ReleaseDate, string Developer, int Downloads);
