using FitAIAPI.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitAIAPI.Application.Validators.UserValidator
{
    public class UserFirstLoginDTOValidator : AbstractValidator<UserFirstLoginDTO>
    {
        public UserFirstLoginDTOValidator()
        {
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Gender is required.");
            RuleFor(x => x.Height).GreaterThan(0).WithMessage("Height must be greater than 0.");
            RuleFor(x => x.FirstWeight).GreaterThan(0).WithMessage("First Weight must be greater than 0.");
            RuleFor(x => x.TargetWeight).GreaterThan(0).WithMessage("Target Weight must be greater than 0.");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of Birth is required.");
            RuleFor(x => x.Goals).NotEmpty().WithMessage("Goals are required.");
        }
    }
}
