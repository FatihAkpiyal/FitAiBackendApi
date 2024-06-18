using FitAIAPI.Application.DTOs;
using FitAIAPI.Application.Services.ChatGPT;
using FitAIAPI.Domain.Entities;
using Newtonsoft.Json;

namespace FitAIAPI.Application.Interfaces
{
    public interface IUserService : IBaseService<User, UserAccountDTO>
    {
        
        Task<UserDetailDTO> GetUserDetailsAsync();
        Task<LoginResponse> RegisterAndLoginUserAsync(UserRegisterRequest requestModel);
        Task<LoginResponse> LoginUserAsync(UserLoginRequest requestModel);
        Task<bool> SaveUserFirstLoginDetailsAsync(UserFirstLoginDTO userFirstLoginDTO);

        Task<UserProfileDTO> UpdateUserProfileAsync(UserProfileDTO userProfileDTO);
        Task<bool> UpdateUserAsync(UserAccountDTO requestModel);
        Task EditUserRoleToAdminAsync(int id);
        Task<UserAccountDTO> GetUserByIdAsync(int id);
        Task<List<UserGetRequest>> GetUsersAsync();


    }
}