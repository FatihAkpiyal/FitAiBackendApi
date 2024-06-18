namespace FitAIAPI.Application.DTOs.WorkoutPlans.Request
{
    public class UpdateExerciseRequest
    {
        public int UserId { get; set; }
        public string OldExerciseName { get; set; }
        public string OldSets { get; set; }
        public string NewExerciseName { get; set; }
        public string NewSets { get; set; }
    }
}
