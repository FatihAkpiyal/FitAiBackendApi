using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitAIAPI.Application.DTOs
{

    public class UpdateWorkoutPlanRequest
    {
        public string Day { get; set; }
        public string Exercise { get; set; }
        public string NewExercise { get; set; }
        public string NewSets { get; set; }
    }
}
