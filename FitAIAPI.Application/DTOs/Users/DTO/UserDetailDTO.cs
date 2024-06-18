namespace FitAIAPI.Application.DTOs
{
    public class UserDetailDTO
    {

        public string? UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Gender { get; set; }
        public double? Height { get; set; }
        public double? FirstWeight { get; set; }
        public string? DateOfBirth { get; set; }

        public double? CurrentWeight { get; set; }
        public double? TargetWeight { get; set; }
        public string? Goals { get; set; }
        public string? PreferredActivities { get; set; }
        public string? WorkoutFrequency { get; set; }
        public string? FocusAreas { get; set; }
        public string? HealthProblem { get; set; }

        public int? BasalMetabolism { get; set; }
        public int? DailyKcalGoal { get; set; }


        public FitnessPlan? WorkoutPlan { get; set; }

    }
}
