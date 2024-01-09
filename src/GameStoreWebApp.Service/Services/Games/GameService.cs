using AutoMapper;
using GameStoreWebApp.Data.IRepositories;
using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Domain.Entities.Feedbacks;
using GameStoreWebApp.Domain.Entities.Games;
using GameStoreWebApp.Service.DTOs.Games;
using GameStoreWebApp.Service.DTOs.Rates;
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

	public ValueTask<bool> DeleteAsync(int id)
	{
		throw new NotImplementedException();
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

	public ValueTask<bool> UpdateAsync(int id, GameUpdateDto gameUpdate)
	{
		throw new NotImplementedException();
	}
}
