using FitAIAPI.Application.DTOs;
using FitAIAPI.Application.Interfaces.ChatGPT;
using FitAIAPI.Application.Interfaces.WorkoutPlan;
using FitAIAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitAIAPI.Application.Services.WorkoutPlan
{
    public class ExerciseAlternativeService : IExerciseAlternativeService
    {
        private readonly IChatGPTService _chatGPTService;

        public ExerciseAlternativeService(IChatGPTService chatGPTService)
        {
            _chatGPTService = chatGPTService;
        }

        public async Task<List<ExerciseAlternative>> GetExerciseAlternatives(ExerciseAlternative exerciseAlternative)
        {
            return await _chatGPTService.GetExerciseAlternatives(exerciseAlternative);
        }
    }
}
