using FitAIAPI.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitAIAPI.Application.Validators.UserValidator
{
    public class UserProfileDTOValidator : AbstractValidator<UserProfileDTO>
    {
        public UserProfileDTOValidator()
        {
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Gender is required.");
            RuleFor(x => x.Height).GreaterThan(0).WithMessage("Height must be greater than 0.");
            RuleFor(x => x.CurrentWeight).GreaterThan(0).WithMessage("Current Weight must be greater than 0.");
            RuleFor(x => x.TargetWeight).GreaterThan(0).WithMessage("Target Weight must be greater than 0.");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of Birth is required.");
            RuleFor(x => x.Goals).NotEmpty().WithMessage("Goals are required.");
        }
    }
}
