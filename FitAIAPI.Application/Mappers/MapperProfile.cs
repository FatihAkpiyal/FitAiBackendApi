using AutoMapper;
using FitAIAPI.Application.DTOs;
using FitAIAPI.Domain.Entities;

namespace FitAIAPI.Application.Mappers
{
    public class MapperProfile: Profile
    {

        public MapperProfile()
        {

            CreateMap<User, UserAccountDTO>();
            CreateMap<User, UserAccountDTO>().ReverseMap();

            CreateMap<PagedResponse<User>, PagedResponse<UserAccountDTO>>();
            CreateMap<PagedResponse<User>, PagedResponse<UserAccountDTO>>().ReverseMap();

        }
        

    }
}
