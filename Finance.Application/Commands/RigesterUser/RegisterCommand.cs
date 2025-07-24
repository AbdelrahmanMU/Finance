using MediatR;
using Microsoft.AspNetCore.Http;

namespace Finance.Application.Commands.RigesterUser;

public class RegisterCommand : IRequest<string>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public IFormFile Image { get; set; } = default!;
}
