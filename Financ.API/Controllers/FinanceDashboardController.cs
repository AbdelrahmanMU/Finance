using Finance.Application.Queries.GetFinanceRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financ.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FinanceDashboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public FinanceDashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("requests")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<FinanceRequestListResult>> Get([FromQuery] GetFinanceRequestsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}