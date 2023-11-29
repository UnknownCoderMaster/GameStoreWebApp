using GameStoreWebApp.Domain.Entities.Games;
using GameStoreWebApp.Service.DTOs.Games;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStoreWebApp.Service.Interfaces.Games;

public interface IGameService
{
	ValueTask<Game> CreateAsync(GameCreate gameCreate);
	ValueTask<Game> UpdateAsync(int id, GameUpdate gameUpdate);
	ValueTask<bool> DeleteAsync(int id);
	ValueTask<Game> GetAsync(Expression<bool> expression);
}
