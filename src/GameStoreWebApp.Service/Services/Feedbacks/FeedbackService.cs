using AutoMapper;
using GameStoreWebApp.Data.IRepositories;
using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Domain.Entities.Feedbacks;
using GameStoreWebApp.Service.DTOs.Feedbacks;
using GameStoreWebApp.Service.DTOs.Rates;
using GameStoreWebApp.Service.Exceptions;
using GameStoreWebApp.Service.Extensions;
using GameStoreWebApp.Service.Interfaces.Feedbacks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStoreWebApp.Service.Services.Feedbacks;

public class FeedbackService : IFeedbackService
{
	private readonly IGenericRepository<Feedback> feedbackRepository;
	private readonly IMapper mapper;

	public FeedbackService(IGenericRepository<Feedback> feedbackRepository, IMapper mapper)
	{
		this.feedbackRepository = feedbackRepository;
		this.mapper = mapper;
	}

	public async ValueTask<bool> CreateAsync(FeedbackCreateDto feedbackCreateDto)
	{
		var creatingFeedback = await feedbackRepository.CreateAsync(mapper.Map<Feedback>(feedbackCreateDto));
		await feedbackRepository.SaveChangesAsync();

		return true;
	}

	public async ValueTask<IEnumerable<FeedbackGetDto>> GetAllAsync(PaginationParams @params, Expression<Func<Feedback, bool>> expression = null)
	{
		var feedbacks = feedbackRepository.GetAll(expression: expression, isTracking: false);
		return mapper.Map<IEnumerable<FeedbackGetDto>>(await feedbacks.ToPagedList(@params).ToListAsync());
	}

	public async ValueTask<FeedbackGetDto> GetAsync(Expression<Func<Feedback, bool>> expression)
	{
		var feedback = await feedbackRepository.GetAsync(expression);

		if (feedback is null)
			throw new GameAppException(404, "Feedback not found");

		return mapper.Map<FeedbackGetDto>(feedback);
	}
}
