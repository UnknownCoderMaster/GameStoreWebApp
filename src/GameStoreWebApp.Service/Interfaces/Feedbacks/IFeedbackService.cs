using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Domain.Entities.Feedbacks;
using GameStoreWebApp.Service.DTOs.Feedbacks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStoreWebApp.Service.Interfaces.Feedbacks;

public interface IFeedbackService
{
	ValueTask<bool> CreateAsync(FeedbackCreateDto feedbackCreateDto);
	ValueTask<FeedbackGetDto> GetAsync(Expression<Func<Feedback, bool>> expression);
	ValueTask<IEnumerable<FeedbackGetDto>> GetAllAsync(PaginationParams @params, Expression<Func<Feedback, bool>> expression = null);
}
