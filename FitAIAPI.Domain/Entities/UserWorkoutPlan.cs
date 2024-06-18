using FitAIAPI.Domain.Entities.Abstracts;

namespace FitAIAPI.Domain.Entities
{
    public class UserWorkoutPlan: IBaseEntity,ISoftDelete
    {
        public int Id { get; set; }
        //public string Day { get; set; } // Day of the workout plan (e.g., "day_one")
        public string Program { get; set; } // JSON or a structured representation of the workout program

        public int UserId { get; set; }
        public User User { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
