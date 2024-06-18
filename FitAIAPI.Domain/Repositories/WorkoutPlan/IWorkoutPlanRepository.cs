using FitAIAPI.Domain.Entities;

namespace FitAIAPI.Domain.Repositories.WorkoutPlan
{
    public interface IWorkoutPlanRepository : IGenericRepository<UserWorkoutPlan>
    {
        //Task<IEnumerable<UserWorkoutPlan>> GetByUserIdAsync(int userId);
        Task<UserWorkoutPlan> GetByUserIdAsync(int userId);
    }

}