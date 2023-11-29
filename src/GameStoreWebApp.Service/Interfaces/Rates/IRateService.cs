using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Domain.Entities.Feedbacks;
using GameStoreWebApp.Domain.Entities.Users;
using GameStoreWebApp.Service.DTOs.Rates;
using GameStoreWebApp.Service.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStoreWebApp.Service.Interfaces.Rates;

public interface IRateService
{
	ValueTask<IEnumerable<RateGet>> GetAllAsync(PaginationParams @params, Expression<Func<Rate, bool>> expression = null);
	ValueTask<RateGet> GetAsync(Expression<Func<Rate, bool>> expression);
	ValueTask<bool> CreateAsync(UserCreateDto rateForCreationDTO);
	ValueTask<bool> DeleteAsync(int id);
	ValueTask<RateGet> UpdateAsync(int id, UserUpdateDto rateForUpdateDTO);
}