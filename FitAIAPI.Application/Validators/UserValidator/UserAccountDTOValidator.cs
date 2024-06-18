using FitAIAPI.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitAIAPI.Application.Validators.UserValidator
{
    public class UserAccountDTOValidator : AbstractValidator<UserAccountDTO>
    {
        public UserAccountDTOValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required.");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name is required.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid Email is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(x => x.BirthDate).NotEmpty().WithMessage("Birth Date is required.");
        }
    }
}
