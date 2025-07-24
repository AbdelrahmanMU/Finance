using Finance.Application.DTOs;
using Finance.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Finance.Application.Queries.GetFinanceRequest;

public class GetFinanceRequestsQueryHandler : IRequestHandler<GetFinanceRequestsQuery, FinanceRequestListResult>
{
    private readonly ApplicationDbContext _context;

    public GetFinanceRequestsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<FinanceRequestListResult> Handle(GetFinanceRequestsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.FinanceRequests
            .AsQueryable()
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.RequestNumber))
        {
            query = query.Where(r => r.RequestNumber.Contains(request.RequestNumber));
        }

        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            query = query.Where(r => r.RequestStatus == request.Status);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var data = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(r => new FinanceRequestDto
            {
                RequestNumber = r.RequestNumber,
                PaymentAmount = r.PaymentAmount,
                PaymentPeriod = r.PaymentPeriod,
                TotalProfit = r.TotalProfit,
                RequestStatus = r.RequestStatus
            })
            .ToListAsync(cancellationToken);

        return new FinanceRequestListResult
        {
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            Data = data
        };
    }
}
