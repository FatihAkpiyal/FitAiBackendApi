using Newtonsoft.Json;

namespace FitAIAPI.Application.DTOs
{
    public class ExerciseAlternative
    {
        [JsonProperty("exercise_name")]
        public string Name { get; set; }

        [JsonProperty("exercise_frequency")]
        public string Sets { get; set; }
    }
}
