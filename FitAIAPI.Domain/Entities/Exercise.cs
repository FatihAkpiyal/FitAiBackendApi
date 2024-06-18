namespace FitAIAPI.Domain.Entities
{
    public class Exercise
    {
        public int Id { get; set; }
        public int UserWorkoutDayId { get; set; }
        public string Name { get; set; }
        public int Repetitions { get; set; }
        public int Sets { get; set; }
        public bool IsDeleted { get; set; }

        public UserWorkoutDay UserWorkoutDay { get; set; }
    }
}
