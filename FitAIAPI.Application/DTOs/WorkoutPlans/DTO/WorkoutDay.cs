using Newtonsoft.Json;

namespace FitAIAPI.Application.DTOs
{
    public class WorkoutDay
    {
        [JsonProperty("day")]
        public string Day { get; set; }
        [JsonProperty("program")]
        public Dictionary<string, string> Program { get; set; }
    }
}
