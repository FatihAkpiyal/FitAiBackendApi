using FitAIAPI.Domain.Entities;

namespace FitAIAPI.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> UserCheckAsync(string email);
        Task<bool> UserExistenceCheckAsync(string email);
        Task EditUserRoleToAdminAsync(int id);

        //Task<Dictionary<string, List<Exercise>>> GetUserProgramAsync(int userId);
        //Task UpdateUserProgramAsync(int userId, Dictionary<string, List<Exercise>> updatedProgram);

    }
}
