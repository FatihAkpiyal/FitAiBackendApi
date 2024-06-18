using FitAIAPI.Application.Interfaces.ChatGPT;
using FitAIAPI.Application.Interfaces.WorkoutPlan;
using FitAIAPI.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FitAIAPI.Application.Services.WorkoutPlan
{
    public class WorkoutPlanGenerationService : IWorkoutPlanGenerationService
    {
        private readonly IChatGPTService _chatGPTService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public WorkoutPlanGenerationService(IChatGPTService chatGPTService, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _chatGPTService = chatGPTService;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        private int GetUserIdFromToken()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("Id") ?? throw new UnauthorizedAccessException("User ID not found in token");
            return int.Parse(userIdClaim.Value);
        }

        public async Task<string> GenerateWorkoutPlanAsync()
        {
            int userId = GetUserIdFromToken();
            string rawWorkoutPlan = await _chatGPTService.GenerateWorkoutPlan(userId);
            var parsedPlan = _chatGPTService.ParseWorkoutPlan(rawWorkoutPlan);
            var user = await _userRepository.GetByIdAsync(userId);
            return FormatWorkoutPlan(parsedPlan, user.UserDetail.WorkoutFrequency);
        }

        private string FormatWorkoutPlan(List<Dictionary<string, string>> parsedPlan, string workoutFrequency)
        {
            var days = new List<string> { "day_one", "day_two", "day_three", "day_four", "day_five", "day_six", "day_seven" };
            int frequency = GetFrequencyAsInt(workoutFrequency);
            var workoutPlan = new List<object>();

            for (int i = 0; i < frequency && i < parsedPlan.Count; i++)
            {
                workoutPlan.Add(new
                {
                    day = days[i],
                    program = parsedPlan[i]
                });
            }

            return JsonConvert.SerializeObject(new { fitness_antrenman = workoutPlan });
        }

        private int GetFrequencyAsInt(string workoutFrequency)
        {
            return workoutFrequency.ToLower() switch
            {
                "onerilen" => 3,
                "haftada 1-2" => 2,
                "haftada 3-4" => 4,
                "haftada 4-5" => 5,
                "haftada 5-6" => 6,
                "her gün" => 7,
                _ => 3
            };
        }
    }
}
