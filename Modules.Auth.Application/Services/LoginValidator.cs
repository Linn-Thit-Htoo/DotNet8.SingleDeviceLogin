using FluentValidation;
using Shared.DTOs.Features.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Auth.Application.Services
{
    public class LoginValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginValidator()
        {

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email format is invalid.")
                .NotEmpty().WithMessage("Email cannot be empty.")
                .NotNull().WithMessage("Email cannot be null.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty.")
                .NotNull().WithMessage("Password cannot be null.");
        }
    }
}
