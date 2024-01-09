using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Domain.Entities.Feedbacks;
using GameStoreWebApp.Service.DTOs.Rates;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStoreWebApp.Service.Interfaces.Rates;

public interface IRateService
{
	ValueTask<IEnumerable<RateGetDto>> GetAllAsync(PaginationParams @params, Expression<Func<Rate, bool>> expression = null);
	ValueTask<RateGetDto> GetAsync(Expression<Func<Rate, bool>> expression);
	ValueTask<bool> CreateAsync(RateCreateDto rateCreateDto);
	ValueTask<bool> DeleteAsync(int id);
	ValueTask<RateGetDto> UpdateAsync(int id, RateUpdateDto rateUpdateDto);
}