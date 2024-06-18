using FitAIAPI.Application.DTOs;
using FluentValidation;

namespace FitAIAPI.Application.Validators.UserValidator
{
    public class UserDetailDTOValidator : AbstractValidator<UserDetailDTO>
    {
        public UserDetailDTOValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid Email is required.");
            RuleFor(x => x.Gender).NotEmpty().WithMessage("Gender is required.");
            RuleFor(x => x.Height).GreaterThan(0).WithMessage("Height must be greater than 0.");
            RuleFor(x => x.FirstWeight).GreaterThan(0).WithMessage("First Weight must be greater than 0.");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of Birth is required.");
            RuleFor(x => x.CurrentWeight).GreaterThan(0).WithMessage("Current Weight must be greater than 0.");
            RuleFor(x => x.TargetWeight).GreaterThan(0).WithMessage("Target Weight must be greater than 0.");
        }
    }
}
