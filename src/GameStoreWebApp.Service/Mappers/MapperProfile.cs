using AutoMapper;
using GameStoreWebApp.Domain.Entities.Addresses;
using GameStoreWebApp.Domain.Entities.Feedbacks;
using GameStoreWebApp.Domain.Entities.Games;
using GameStoreWebApp.Domain.Entities.Users;
using GameStoreWebApp.Service.DTOs.Addresses;
using GameStoreWebApp.Service.DTOs.Feedbacks;
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

        // Feedback
        CreateMap<Feedback, FeedbackGetDto>().ReverseMap();
        CreateMap<FeedbackCreateDto, Feedback>().ReverseMap();

        // Address
        CreateMap<Region, RegionGetDto>().ReverseMap();
        CreateMap<Country, CountryGetDto>().ReverseMap();
	}
}
