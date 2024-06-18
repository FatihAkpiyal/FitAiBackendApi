using FitAIAPI.Domain.Entities.Abstracts;

namespace FitAIAPI.Domain.Entities
{
    public class User : IBaseEntity, ISoftDelete
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "User";
        public bool IsDeleted { get; set; } = false;
        public bool IsFirstLogin { get; set; } = true;
        public string? ProfilePictureUrl { get; set; } // Profil fotoğrafı URL'si
        public DateTime? LastLogin { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        // Navigation properties
        public UserDetail UserDetail { get; set; }
        public ICollection<UserWorkoutPlan> UserWorkoutPlans { get; set; }
        public ICollection<NutritionPlan> NutritionPlans { get; set; }
        public ICollection<FoodEntry> FoodEntries { get; set; }

    }
}
