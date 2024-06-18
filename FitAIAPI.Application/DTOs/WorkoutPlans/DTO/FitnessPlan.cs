using Newtonsoft.Json;

namespace FitAIAPI.Application.DTOs
{
    public class FitnessPlan
    {
        [JsonProperty("fitness_antrenman")]
        public List<WorkoutDay> FitnessAntrenman { get; set; }
    }
}
