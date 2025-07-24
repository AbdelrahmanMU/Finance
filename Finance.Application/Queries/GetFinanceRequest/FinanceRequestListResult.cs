using Finance.Application.DTOs;

namespace Finance.Application.Queries.GetFinanceRequest;

public class FinanceRequestListResult
{
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<FinanceRequestDto> Data { get; set; } = new();
}
