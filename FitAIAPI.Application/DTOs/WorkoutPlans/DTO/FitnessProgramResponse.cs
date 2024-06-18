using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitAIAPI.Application.DTOs
{
    public class FitnessProgramResponse
    {
        [JsonProperty("fitness_antrenman")]
        public List<DayProgram>? FitnessAntrenman { get; set; }
    }
}
