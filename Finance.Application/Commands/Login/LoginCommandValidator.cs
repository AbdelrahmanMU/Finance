using Finance.Domain;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Finance.Application.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IPasswordHasher<User> _passwordHasher;
    private User? _user;

    public LoginCommandValidator(UserManager<User> userManager,
        IPasswordHasher<User> passwordHasher)
    {
        _userManager = userManager;
        _passwordHasher = passwordHasher;

        RuleFor(r => r.Email)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .MustAsync(BeExistUser).WithMessage("Email is not found");

        RuleFor(r => r.Password)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull()
            .NotEmpty()
            .MustAsync(BeCorrectPassword)
            .WithMessage("Password is not valid");
    }

    private async Task<bool> BeExistUser(string email, CancellationToken cancellationToken)
    {
        _user = await _userManager.FindByEmailAsync(email);
        return _user is not null;
    }

    private async Task<bool> BeCorrectPassword(string password, CancellationToken cancellationToken)
    {
        if (_user is null) return false;

        var isValidPassword = _passwordHasher.VerifyHashedPassword(_user!, _user!.PasswordHash!, password)
                              == PasswordVerificationResult.Success;
        if (isValidPassword) return true;
        await _userManager.AccessFailedAsync(_user);

        return false;
    }
}
