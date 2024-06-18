using FitAIAPI.Application.DTOs;
using FitAIAPI.Application.Interfaces.ChatGPT;
using FitAIAPI.Application.Interfaces.WorkoutPlan;
using FitAIAPI.Domain.Repositories.WorkoutPlan;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace FitAIAPI.Application.Services.WorkoutPlan
{
    public class WorkoutPlanUpdateService : IWorkoutPlanUpdateService
    {
        private readonly IWorkoutPlanRepository _workoutPlanRepository;
        private readonly IChatGPTService _chatGPTService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WorkoutPlanUpdateService(IWorkoutPlanRepository workoutPlanRepository, IChatGPTService chatGPTService, IHttpContextAccessor httpContextAccessor)
        {
            _workoutPlanRepository = workoutPlanRepository;
            _chatGPTService = chatGPTService;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserIdFromToken()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("Id") ?? throw new UnauthorizedAccessException("User ID not found in token");
            return int.Parse(userIdClaim.Value);
        }

        public async Task UpdateUserWorkoutPlanAsync(UpdateWorkoutPlanRequest updateRequest)
        {
            int userId = GetUserIdFromToken();
            var workoutPlan = await _workoutPlanRepository.GetByUserIdAsync(userId) ?? throw new Exception("Workout plan not found.");
            var parsedPlan = _chatGPTService.ParseWorkoutPlanForUpdate(workoutPlan.Program);

            if (!parsedPlan.ContainsKey(updateRequest.Day))
                throw new Exception("Day not found in workout plan.");

            var dayPlan = parsedPlan[updateRequest.Day];
            if (!dayPlan.ContainsKey(updateRequest.Exercise))
                throw new Exception("Exercise not found in day plan.");

            dayPlan.Remove(updateRequest.Exercise);
            dayPlan[updateRequest.NewExercise] = updateRequest.NewSets;

            var updatedFitnessAntrenman = parsedPlan.Select(day => new
            {
                day = day.Key,
                program = day.Value
            }).ToList();

            workoutPlan.Program = JsonConvert.SerializeObject(new { fitness_antrenman = updatedFitnessAntrenman });
            await _workoutPlanRepository.UpdateAsync(workoutPlan);
        }
    }
}
