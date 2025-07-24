using MediatR;

namespace Finance.Application.Commands.Login;

public class LoginCommand : IRequest<string>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
