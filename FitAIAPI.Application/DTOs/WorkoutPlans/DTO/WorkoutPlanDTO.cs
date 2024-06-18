namespace FitAIAPI.Application.DTOs
{
    public class WorkoutPlanDTO
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public string Description { get; set; }
        public List<WorkoutDayDTO> WorkoutDays { get; set; }
    }
}
