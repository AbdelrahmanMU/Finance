using MediatR;

namespace Finance.Application.Queries.GetFinanceRequest;

public class GetFinanceRequestsQuery : IRequest<FinanceRequestListResult>
{
    public string? RequestNumber { get; set; }
    public string? Status { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
