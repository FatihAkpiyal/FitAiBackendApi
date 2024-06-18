using FitAIAPI.Application.DTOs;
using FitAIAPI.Application.Services.ChatGPT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FitAIAPI.Application.Services.ChatGPT.ChatGPTService;

namespace FitAIAPI.Application.Interfaces.ChatGPT
{
    public interface IChatGPTService
    {
        Task<string> GenerateWorkoutPlan(int userId);
        List<Dictionary<string, string>> ParseWorkoutPlan(string rawPlan);

        Task<List<ExerciseAlternative>> GetExerciseAlternatives(ExerciseAlternative exerciseAlternative);

        Dictionary<string, Dictionary<string, string>> ParseWorkoutPlanForUpdate(string rawPlan);

    }
}
