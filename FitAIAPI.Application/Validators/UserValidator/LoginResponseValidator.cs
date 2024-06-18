using FitAIAPI.Application.DTOs;
using FluentValidation;

namespace FitAIAPI.Application.Validators.UserValidator
{
    public class LoginResponseValidator : AbstractValidator<LoginResponse>
    {
        public LoginResponseValidator()
        {
            RuleFor(x => x.Token).NotEmpty().WithMessage("Token is required.");
            RuleFor(x => x.IsFirstLogin).NotNull().WithMessage("IsFirstLogin is required.");
        }
    }
}
