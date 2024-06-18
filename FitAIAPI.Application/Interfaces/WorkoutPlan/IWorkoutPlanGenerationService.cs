using FitAIAPI.Domain.Entities;

namespace FitAIAPI.Application.Interfaces.WorkoutPlan
{
    public interface IWorkoutPlanGenerationService
    {
        Task<string> GenerateWorkoutPlanAsync();
    }
}
