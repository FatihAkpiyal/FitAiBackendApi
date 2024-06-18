using FitAIAPI.Application.DTOs;
using FitAIAPI.Application.Interfaces.WorkoutPlan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FitAIAPI.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutPlanController : ControllerBase
    {
        private readonly IWorkoutPlanGenerationService _workoutPlanGenerationService;
        private readonly IWorkoutPlanUpdateService _workoutPlanUpdateService;
        private readonly IWorkoutPlanDetailService _workoutPlanDetailService;
        private readonly IExerciseAlternativeService _exerciseAlternativeService;

        public WorkoutPlanController(
            IWorkoutPlanGenerationService workoutPlanGenerationService,
            IWorkoutPlanUpdateService workoutPlanUpdateService,
            IWorkoutPlanDetailService workoutPlanDetailService,
            IExerciseAlternativeService exerciseAlternativeService)
        {
            _workoutPlanGenerationService = workoutPlanGenerationService;
            _workoutPlanUpdateService = workoutPlanUpdateService;
            _workoutPlanDetailService = workoutPlanDetailService;
            _exerciseAlternativeService = exerciseAlternativeService;
        }

        [HttpGet("generateworkoutplan")]
        public async Task<IActionResult> GenerateWorkoutPlan()
        {
            try
            {
                var workoutPlan = await _workoutPlanGenerationService.GenerateWorkoutPlanAsync();
                return Ok(workoutPlan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateWorkoutPlan(UpdateWorkoutPlanRequest updateRequest)
        {
            try
            {
                await _workoutPlanUpdateService.UpdateUserWorkoutPlanAsync(updateRequest);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost("workoutdetails")]
        public async Task<IActionResult> SaveWorkoutDetails(UserWorkoutDetailsDTO workoutDetailsDTO)
        {
            try
            {
                await _workoutPlanDetailService.SaveWorkoutDetailsAsync(workoutDetailsDTO);
                return Ok(true);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost("alternatives")]
        public async Task<IActionResult> GetExerciseAlternatives(ExerciseAlternative exerciseAlternative)
        {
            try
            {
                var alternatives = await _exerciseAlternativeService.GetExerciseAlternatives(exerciseAlternative);
                return Ok(alternatives);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
