using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitAIAPI.Application.DTOs
{
    public class DayProgram
    {
        [JsonProperty("day")]
        public string Day { get; set; }

        [JsonProperty("program")]
        public Dictionary<string, string> Program { get; set; }
    }
}
