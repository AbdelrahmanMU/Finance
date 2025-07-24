using Finance.Application.DTOs;
using Finance.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Finance.Application.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly UserManager<User> _userManager;
    private readonly JwtConfig _jwtConfig;

    public LoginCommandHandler(UserManager<User> userManager,
        IOptions<JwtConfig> jwtOptions)
    {
        _userManager = userManager;
        _jwtConfig = jwtOptions.Value;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        var roles = await _userManager.GetRolesAsync(user);

        var token = JsonWebTokenGeneration.GenerateJwtToken(user, roles.FirstOrDefault(), _jwtConfig);

        return token;
    }
}
