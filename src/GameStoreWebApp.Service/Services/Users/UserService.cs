using AutoMapper;
using GameStoreWebApp.Data.IRepositories;
using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Domain.Entities.Users;
using GameStoreWebApp.Service.DTOs.Users;
using GameStoreWebApp.Service.Exceptions;
using GameStoreWebApp.Service.Extensions;
using GameStoreWebApp.Service.Helpers;
using GameStoreWebApp.Service.Interfaces.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStoreWebApp.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> userRepository;
	private readonly IAuthService authService;
	private readonly IMapper mapper;

	public UserService(IGenericRepository<User> userRepository, IServiceProvider serviceProvider,
        IMapper mapper)
    {
		this.userRepository = userRepository;
		this.authService = serviceProvider.GetRequiredService<IAuthService>();
		this.mapper = mapper;
	}

	public async ValueTask<bool> ChangePasswordAsync(UserForChangePasswordDto userForChangePasswordDTO)
    {
		var user = await userRepository.GetAsync(u => u.Id == HttpContextHelper.UserId);

		if (user == null)
			throw new GameAppException(404, "User not found");

		if (user.Password != userForChangePasswordDTO.OldPassword.Encrypt())
			throw new GameAppException(400, "Password is incorrect");


		user.Password = userForChangePasswordDTO.NewPassword.Encrypt();

		userRepository.Update(user);
		await userRepository.SaveChangesAsync();
		return true;
	}

    public async ValueTask<bool> CreateAsync(UserCreateDto userForCreationDTO)
    {
		var existEmail = await userRepository.GetAsync(u => u.Email == userForCreationDTO.Email);

		if (existEmail != null)
			throw new GameAppException(400, "This email is already taken");

		var createdUser = await userRepository.CreateAsync(mapper.Map<User>(userForCreationDTO));

		createdUser.Password = createdUser.Password.Encrypt();

		createdUser.Role = Domain.Enums.UserRole.User;

		await userRepository.SaveChangesAsync();

		return await authService.SendEmailVerification(createdUser.Id);
	}

    public async ValueTask<bool> DeleteAsync(int id)
    {
        var isDeleted = await userRepository.DeleteAsync(id);

        if (!isDeleted)
            throw new GameAppException(404, "User Not Found");

        await userRepository.SaveChangesAsync();

        return true;
    }

    public async ValueTask<IEnumerable<UserGetDto>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null)
    {
		var users = userRepository.GetAll(expression: expression, isTracking: false, includes: new string[] {"Country", "Region"});
        return mapper.Map<List<UserGetDto>>(await users.ToPagedList(@params).ToListAsync());
	}

    public async ValueTask<UserGetDto> GetAsync(Expression<Func<User, bool>> expression)
    {
        var user = await userRepository.GetAsync(expression: expression, includes: new string[] {"Country", "Region"});

        if (user is null)
            throw new GameAppException(404, "User not found");

        return mapper.Map<UserGetDto>(user);
    }

    public async ValueTask<UserGetDto> UpdateAsync(int id, UserUpdateDto userUpdateDto)
    {
		var existUser = await userRepository.GetAsync(
				u => u.Id == id);

		if (existUser == null)
			throw new GameAppException(404, "User not found");

		var alreadyExistUser = await userRepository.GetAsync(
			u => u.Email == userUpdateDto.Email && u.Id != HttpContextHelper.UserId);

		if (alreadyExistUser != null)
			throw new GameAppException(400, "User with such email already exists");


		existUser.UpdatedAt = DateTime.UtcNow;
		existUser = userRepository.Update(mapper.Map(userUpdateDto, existUser));
		await userRepository.SaveChangesAsync();

		return mapper.Map<UserGetDto>(existUser);
	}
}
