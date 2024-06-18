using FitAIAPI.Application.DTOs;

namespace FitAIAPI.Application.Interfaces.WorkoutPlan
{
    public interface IWorkoutPlanDetailService
    {
        Task SaveWorkoutDetailsAsync(UserWorkoutDetailsDTO workoutDetailsDTO);
    }
}
