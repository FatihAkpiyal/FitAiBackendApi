using FitAIAPI.Application.DTOs;
using FitAIAPI.Application.Interfaces.WorkoutPlan;
using FitAIAPI.Domain.Entities;
using FitAIAPI.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace FitAIAPI.Application.Services.WorkoutPlan
{
    public class WorkoutPlanDetailService : IWorkoutPlanDetailService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WorkoutPlanDetailService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserIdFromToken()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("Id") ?? throw new UnauthorizedAccessException("User ID not found in token");
            return int.Parse(userIdClaim.Value);
        }

        public async Task SaveWorkoutDetailsAsync(UserWorkoutDetailsDTO workoutDetailsDTO)
        {
            int userId = GetUserIdFromToken();
            var user = await _userRepository.GetByIdAsync(userId) ?? throw new UserNotFoundException(userId.ToString());

            user.UserDetail ??= new UserDetail { UserId = userId };
            user.UserDetail.PreferredActivities = workoutDetailsDTO.PreferredActivities;
            user.UserDetail.WorkoutFrequency = workoutDetailsDTO.WorkoutFrequency;
            user.UserDetail.FocusAreas = workoutDetailsDTO.FocusAreas;
            user.UserDetail.HealthProblem = workoutDetailsDTO.HealthProblem;

            if (!user.UserDetail.BasalMetabolism.HasValue)
                throw new InvalidOperationException("Basal Metabolism değeri null. Lütfen ilk giriş bilgilerini kaydedin.");

            user.UserDetail.DailyKcalGoal = CalculateDailyKcalGoal(user.UserDetail);

            await _userRepository.UpdateAsync(user);
        }

        private int CalculateDailyKcalGoal(UserDetail userDetail)
        {
            int workoutMultiplier = userDetail.WorkoutFrequency.ToLower() switch
            {
                "onerilen" => 3,
                "haftada 1-2" => 1,
                "haftada 2-3" => 1,
                "haftada 3-4" => 2,
                "haftada 4-5" => 2,
                "haftada 5-6" => 3,
                _ => 1
            };

            return userDetail.BasalMetabolism.Value * workoutMultiplier;
        }
    }
}
