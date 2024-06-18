using FitAIAPI.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitAIAPI.Application.Validators.WorkoutPlanValidator
{
    public class UpdateWorkoutPlanRequestValidator : AbstractValidator<UpdateWorkoutPlanRequest>
    {
        public UpdateWorkoutPlanRequestValidator()
        {
            RuleFor(x => x.Day).NotEmpty().WithMessage("Day is required.");
            RuleFor(x => x.Exercise).NotEmpty().WithMessage("Exercise is required.");
            RuleFor(x => x.NewExercise).NotEmpty().WithMessage("New Exercise is required.");
            RuleFor(x => x.NewSets).NotEmpty().WithMessage("New Sets are required.");
        }
    }
}
