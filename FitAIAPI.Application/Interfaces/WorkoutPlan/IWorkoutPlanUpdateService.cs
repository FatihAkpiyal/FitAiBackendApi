using FitAIAPI.Application.DTOs;

namespace FitAIAPI.Application.Interfaces.WorkoutPlan
{
    public interface IWorkoutPlanUpdateService
    {
        Task UpdateUserWorkoutPlanAsync(UpdateWorkoutPlanRequest updateRequest);
    }
}
