﻿using GameStoreWebApp.Domain.Enums;
using System;

namespace GameStoreWebApp.Service.DTOs.Games;

public record GameCreate(string Name, string Description, Category Category, DateOnly ReleaseDate, string Developer);
