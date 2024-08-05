namespace Modules.Auth.Application.Services.ValidatorServices;

public class RegisterValidator : AbstractValidator<RegisterRequestModel>
{
    public RegisterValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("User Name cannot be empty.")
            .NotNull()
            .WithMessage("User Name cannot be null.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email format is invalid.")
            .NotEmpty()
            .WithMessage("Email cannot be empty.")
            .NotNull()
            .WithMessage("Email cannot be null.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty.")
            .NotNull()
            .WithMessage("Password cannot be null.");

        RuleFor(x => x.UserRole)
            .NotEmpty()
            .WithMessage("User Role cannot be empty.")
            .NotNull()
            .WithMessage("User Role cannot be null.");
    }
}
