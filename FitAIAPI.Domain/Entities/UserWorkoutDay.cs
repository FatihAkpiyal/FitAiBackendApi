using FitAIAPI.Domain.Entities.Abstracts;

namespace FitAIAPI.Domain.Entities
{
    public class UserWorkoutDay:IBaseEntity,ISoftDelete
    {
        public int Id { get; set; }
        public int UserWorkoutPlanId { get; set; }
        public string Day { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }

        public UserWorkoutPlan UserWorkoutPlan { get; set; }
        public ICollection<Exercise> Exercises { get; set; }



    }
}
