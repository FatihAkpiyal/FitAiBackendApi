using AutoMapper;
using FitAIAPI.Application.DTOs;
using FitAIAPI.Domain.Entities;

namespace FitAIAPI.Application.Mappers;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<User, UserDetail>().ReverseMap();

        CreateMap<User, UserAccountDTO>().ReverseMap();

        CreateMap<User, UserProfileDTO>().ReverseMap();
        
        CreateMap<UserRegisterRequest, User>().ReverseMap();

        CreateMap<UserLoginRequest, User>().ReverseMap();
      
        CreateMap<User, UserGetRequest>().ReverseMap();

        CreateMap<UserDetail, UserProfileDTO>().ReverseMap();
       
        CreateMap<UserDetail, UserDetailDTO>().ReverseMap();

        CreateMap<UserDetail, UserFirstLoginDTO>().ReverseMap();

        CreateMap<User, UserFirstLoginDTO>().ReverseMap(); //

        CreateMap<UserDetail, UserWorkoutDetailsDTO>().ReverseMap();

        CreateMap<User, UserDetailDTO>().ReverseMap();

    }
}



