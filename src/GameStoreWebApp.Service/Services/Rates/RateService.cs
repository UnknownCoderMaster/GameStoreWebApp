using AutoMapper;
using GameStoreWebApp.Data.IRepositories;
using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Domain.Entities.Feedbacks;
using GameStoreWebApp.Domain.Entities.Games;
using GameStoreWebApp.Domain.Entities.Users;
using GameStoreWebApp.Service.DTOs.Rates;
using GameStoreWebApp.Service.DTOs.Users;
using GameStoreWebApp.Service.Exceptions;
using GameStoreWebApp.Service.Extensions;
using GameStoreWebApp.Service.Interfaces.Rates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStoreWebApp.Service.Services.Rates;

public class RateService : IRateService
{
	private readonly IGenericRepository<Rate> rateRepository;
	private readonly IGenericRepository<User> userRepository;
	private readonly IGenericRepository<Game> gameRepository;
	private readonly IMapper mapper;

	public RateService(IGenericRepository<Rate> rateRepository,
		IGenericRepository<User> userRepository,
		IGenericRepository<Game> gameRepository,
		IMapper mapper)
    {
        this.rateRepository = rateRepository;
		this.userRepository = userRepository;
		this.gameRepository = gameRepository;
		this.mapper = mapper;
    }

    public async ValueTask<bool> CreateAsync(RateCreateDto rateCreateDto)
	{
		var existingUser = await userRepository.GetAsync(u => u.Id == rateCreateDto.UserId);

		if (existingUser == null)
			throw new GameAppException(404, "User Not Found!");

		var existingGame = await gameRepository.GetAsync(u => u.Id == rateCreateDto.GameId);

		if (existingGame == null)
			throw new GameAppException(404, "Game Not Found!");

		var createdRate = await rateRepository.CreateAsync(mapper.Map<Rate>(rateCreateDto));
		createdRate.UserId = existingUser.Id;
		createdRate.GameId = existingGame.Id;
		await rateRepository.SaveChangesAsync();

		return true;
	}

	public async ValueTask<bool> DeleteAsync(int id)
	{
		var isDeleted = await rateRepository.DeleteAsync(id);

		if (!isDeleted)
			throw new GameAppException(404, "Rate Not Found");

		await rateRepository.SaveChangesAsync();

		return true;
	}

	public async ValueTask<IEnumerable<RateGetDto>> GetAllAsync(PaginationParams @params, Expression<Func<Rate, bool>> expression = null)
	{
		var rates = rateRepository.GetAll(expression: expression, isTracking: false);
		return mapper.Map<List<RateGetDto>>(await rates.ToPagedList(@params).ToListAsync());
	}

	public async ValueTask<RateGetDto> GetAsync(Expression<Func<Rate, bool>> expression)
	{
		var rate = await rateRepository.GetAsync(expression);

		if (rate is null)
			throw new GameAppException(404, "Rate not found");

		return mapper.Map<RateGetDto>(rate);
	}

	public async ValueTask<RateGetDto> UpdateAsync(int id, RateUpdateDto rateUpdateDto)
	{
		var existingRate = await rateRepository.GetAsync(r => r.Id == id);

		if (existingRate is null)
			throw new GameAppException(404, "Rate Not Found!");

		var existingUser = await userRepository.GetAsync(u => u.Id == rateUpdateDto.UserId);

		if(existingUser is null)
			throw new GameAppException(404, "User Not Found!");

		existingRate.UpdatedAt = DateTime.UtcNow;
		existingRate = rateRepository.Update(mapper.Map(rateUpdateDto, existingRate));

		await rateRepository.SaveChangesAsync();

		return mapper.Map<RateGetDto>(existingRate);
	}
}
