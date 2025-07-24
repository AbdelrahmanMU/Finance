using FluentValidation;

namespace Finance.Application.Commands.RigesterUser;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(u => u.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();

        RuleFor(u => u.LastName)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();
    }
}
