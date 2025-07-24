namespace Finance.Application.DTOs;

public class JwtConfig
{
    public string? Key { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public int ExpiryDurationInMinutes { get; set; }
    public int MaxFailedAccessAttempts { get; set; }
}
