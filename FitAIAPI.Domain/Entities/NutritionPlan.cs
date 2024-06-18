using FitAIAPI.Domain.Entities.Abstracts;

namespace FitAIAPI.Domain.Entities
{
    public class NutritionPlan:IBaseEntity,ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }

        public User User { get; set; }
        public ICollection<Food> Foods { get; set; }
    }
}
