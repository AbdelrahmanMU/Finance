namespace Finance.Domain;

public class FinanceRequest
{
    public int Id { get; set; }
    public string RequestNumber { get; set; } = default!;
    public decimal PaymentAmount { get; set; }
    public int PaymentPeriod { get; set; }
    public decimal TotalProfit { get; set; }
    public string RequestStatus { get; set; } = default!;
}
