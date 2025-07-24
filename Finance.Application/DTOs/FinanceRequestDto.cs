namespace Finance.Application.DTOs;

public class FinanceRequestDto
{
    public string RequestNumber { get; set; } = default!;
    public decimal PaymentAmount { get; set; }
    public int PaymentPeriod { get; set; }
    public decimal TotalProfit { get; set; }
    public string RequestStatus { get; set; } = default!;
}
