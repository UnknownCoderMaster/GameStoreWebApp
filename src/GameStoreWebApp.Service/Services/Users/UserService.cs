using AutoMapper;
using GameStoreWebApp.Data.IRepositories;
using GameStoreWebApp.Domain.Configurations;
using GameStoreWebApp.Domain.Entities.Users;
using GameStoreWebApp.Service.DTOs.Users;
using GameStoreWebApp.Service.Exceptions;
using GameStoreWebApp.Service.Interfaces.Users;
using Microsoft.EntityFrameworkCore;
using MyCareer.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameStoreWebApp.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> userRepository;
	private readonly AuthService authService;
	private readonly IMapper mapper;

	public UserService(IGenericRepository<User> userRepository, AuthService authService,
        IMapper mapper)
    {
		this.userRepository = userRepository;
		this.authService = authService;
		this.mapper = mapper;
	}

	public ValueTask<bool> ChangePasswordAsync(UserForChangePasswordDto userForChangePasswordDTO)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<bool> CreateAsync(UserCreateDto userForCreationDTO)
    {
		var existEmail = await userRepository.GetAsync(u => u.Email == userForCreationDTO.Email);

		if (existEmail != null)
			throw new GameAppException(400, "This email is already taken");

		var createdUser = await userRepository.CreateAsync(mapper.Map<User>(userForCreationDTO));

		createdUser.Password = createdUser.Password.Encrypt();

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
		var users = userRepository.GetAll(expression: expression, isTracking: false);
        return mapper.Map<List<UserGetDto>>(await users.ToPagedList(@params).ToListAsync());
	}

    public async ValueTask<UserGetDto> GetAsync(Expression<Func<User, bool>> expression)
    {
        var user = await userRepository.GetAsync(expression);

        if (user is null)
            throw new GameAppException(404, "User not found");

        return mapper.Map<UserGetDto>(user);
    }

    public ValueTask<UserGetDto> UpdateAsync(int id, UserUpdateDto userUpdateDto)
    {
        throw new NotImplementedException();
    }
}
