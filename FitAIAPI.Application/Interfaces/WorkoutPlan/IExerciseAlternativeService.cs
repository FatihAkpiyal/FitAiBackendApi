using FitAIAPI.Application.DTOs;

namespace FitAIAPI.Application.Interfaces.WorkoutPlan
{
    public interface IExerciseAlternativeService
    {
        Task<List<ExerciseAlternative>> GetExerciseAlternatives(ExerciseAlternative exerciseAlternative);
    }
}
