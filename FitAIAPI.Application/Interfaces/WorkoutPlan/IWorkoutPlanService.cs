using FitAIAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitAIAPI.Application.Interfaces.WorkoutPlan
{
    public interface IWorkoutPlanService
    {
        Task<string> GenerateWorkoutPlanAsync();
        Task UpdateUserWorkoutPlanAsync(UpdateWorkoutPlanRequest updateRequest);
        Task SaveWorkoutDetailsAsync(UserWorkoutDetailsDTO workoutDetailsDTO);
        Task<List<ExerciseAlternative>> GetExerciseAlternatives(ExerciseAlternative exerciseAlternative);
    }
}
