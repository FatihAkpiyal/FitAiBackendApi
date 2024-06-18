using FitAIAPI.Domain.Entities.Abstracts;

namespace FitAIAPI.Domain.Entities
{
    public class Food :IBaseEntity,ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Fat { get; set; }
        public int Protein { get; set; }
        public int Carbs { get; set; }
        public bool IsDeleted { get; set; }
        public int NutritionPlanId { get; set; }

        public NutritionPlan NutritionPlan { get; set; }
    }
}
