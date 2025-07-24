using Finance.Application.Commands.Login;
using Finance.Application.Commands.RigesterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financ.API.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    public async Task<ActionResult<string>> Register([FromForm] RegisterCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    public async Task<ActionResult<string>> Login(LoginCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}
