using AutoMapper;
using GameStoreWebApp.Domain.Entities.Feedbacks;
using GameStoreWebApp.Domain.Entities.Games;
using GameStoreWebApp.Domain.Entities.Users;
using GameStoreWebApp.Service.DTOs.Games;
using GameStoreWebApp.Service.DTOs.Rates;
using GameStoreWebApp.Service.DTOs.Users;

namespace GameStoreWebApp.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // User
        CreateMap<User, UserGetDto>().ReverseMap();
        CreateMap<UserCreateDto, User>().ReverseMap();
        CreateMap<UserUpdateDto, User>().ReverseMap();

        // Rate
        CreateMap<Rate, RateGetDto>().ReverseMap();
        CreateMap<RateCreateDto, Rate>().ReverseMap();
        CreateMap<RateUpdateDto, Rate>().ReverseMap();

		// Game
		CreateMap<Game, GameGetDto>().ReverseMap();
		CreateMap<GameCreateDto, Game>().ReverseMap();
		CreateMap<GameUpdateDto, Game>().ReverseMap();
	}
}
