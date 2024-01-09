using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Domain.Entities.Feedbacks;
using GameStoreWebApp.Domain.Entities.Games;
using GameStoreWebApp.Service.DTOs.Games;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStoreWebApp.Service.Interfaces.Games;

public interface IGameService
{
	ValueTask<bool> CreateAsync(GameCreateDto gameCreate);
	ValueTask<bool> UpdateAsync(int id, GameUpdateDto gameUpdate);
	ValueTask<bool> DeleteAsync(int id);
	ValueTask<GameGetDto> GetAsync(Expression<Func<Game, bool>> expression);
	ValueTask<IEnumerable<GameGetDto>> GetAllAsync(PaginationParams @params, Expression<Func<Game, bool>> expression = null);
}
