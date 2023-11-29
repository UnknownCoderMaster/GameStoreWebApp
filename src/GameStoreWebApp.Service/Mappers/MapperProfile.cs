using AutoMapper;
using GameStoreWebApp.Domain.Entities.Users;
using GameStoreWebApp.Service.DTOs.Users;

namespace GameStoreWebApp.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserGetDto>().ReverseMap();
    }
}
