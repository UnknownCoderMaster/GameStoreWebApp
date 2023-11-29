using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Domain.Entities.Users;
using GameStoreWebApp.Service.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStoreWebApp.Service.Interfaces.Users;

public interface IUserService
{
	ValueTask<IEnumerable<UserGetDto>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null);
	ValueTask<UserGetDto> GetAsync(Expression<Func<User, bool>> expression);
	ValueTask<bool> CreateAsync(UserCreateDto userForCreationDTO);
	ValueTask<bool> DeleteAsync(int id);
	ValueTask<UserGetDto> UpdateAsync(int id, UserUpdateDto userForUpdateDTO);
	ValueTask<bool> ChangePasswordAsync(UserForChangePasswordDto userForChangePasswordDTO);
}
