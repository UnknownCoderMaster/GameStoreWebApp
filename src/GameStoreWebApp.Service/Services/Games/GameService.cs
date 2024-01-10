using AutoMapper;
using GameStoreWebApp.Data.IRepositories;
using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Domain.Entities.Games;
using GameStoreWebApp.Service.DTOs.Games;
using GameStoreWebApp.Service.Exceptions;
using GameStoreWebApp.Service.Extensions;
using GameStoreWebApp.Service.Helpers;
using GameStoreWebApp.Service.Interfaces.Games;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStoreWebApp.Service.Services.Games;

public class GameService : IGameService
{
	private readonly IGenericRepository<Game> gameRepository;
	private readonly IMapper mapper;

	public GameService(IGenericRepository<Game> gameRepository, IMapper mapper)
    {
		this.gameRepository = gameRepository;
		this.mapper = mapper;
	}

    public async ValueTask<bool> CreateAsync(GameCreateDto gameCreate)
	{
		var gameFilePath = EnvironmentHelper.GameFilePath;
		var fileName = Guid.NewGuid().ToString() + '_' + gameCreate.File.FileName;
		var filePath = Path.Combine(gameFilePath, fileName);

		using (var stream = new FileStream(filePath, FileMode.Create))
		{
			await gameCreate.File.CopyToAsync(stream);
		}

		var newGame = mapper.Map<Game>(gameCreate);
		newGame.FilePath = filePath;
		await gameRepository.CreateAsync(newGame);
		await gameRepository.SaveChangesAsync();

		return true;
	}

	public async ValueTask<bool> DeleteAsync(int id)
	{
		var existingGame = await gameRepository.GetAsync(g => g.Id == id, false);

		var isDeleted = await gameRepository.DeleteAsync(id);

		if (!isDeleted)
			throw new GameAppException(404, "Game Not Found");

		await gameRepository.SaveChangesAsync();

		if (File.Exists(existingGame.FilePath))
			File.Delete(existingGame.FilePath);
		else
			throw new GameAppException(500, "File Not Deleted");

		return true;
	}

	public async ValueTask<IEnumerable<GameGetDto>> GetAllAsync(PaginationParams @params, Expression<Func<Game, bool>> expression = null)
	{
		var games = gameRepository.GetAll(expression: expression, isTracking: false);
		return mapper.Map<List<GameGetDto>>(await games.ToPagedList(@params).ToListAsync());
	}

	public async ValueTask<GameGetDto> GetAsync(Expression<Func<Game, bool>> expression)
	{
		var game = await gameRepository.GetAsync(expression);

		if (game is null)
			throw new GameAppException(404, "Game not found");

		return mapper.Map<GameGetDto>(game);
	}

	public async ValueTask<bool> UpdateAsync(int id, GameUpdateDto gameUpdateDto)
	{
		var existingGame = await gameRepository.GetAsync(g => g.Id == id, false);

		if (existingGame is null)
			throw new GameAppException(404, "Game Not Found!");

		var updatingGame = mapper.Map(gameUpdateDto, existingGame);
		updatingGame.UpdatedAt = DateTime.UtcNow;

		if(gameUpdateDto.File is not null)
		{
			if (File.Exists(existingGame.FilePath))
				File.Delete(existingGame.FilePath);

			var gameFilePath = EnvironmentHelper.GameFilePath;
			var fileName = Guid.NewGuid().ToString() + '_' + gameUpdateDto.File.FileName;
			var filePath = Path.Combine(gameFilePath, fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await gameUpdateDto.File.CopyToAsync(stream);
			}

			updatingGame.FilePath = filePath;
		}
		
		gameRepository.Update(updatingGame);

		await gameRepository.SaveChangesAsync();

		return true;
	}
}
