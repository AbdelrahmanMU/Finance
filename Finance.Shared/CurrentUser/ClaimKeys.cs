using System.Security.Claims;

namespace Finance.Shared.CurrentUser;

public static class ClaimKeys
{
    public const string Id = "Id";
    public const string FirstName = "FirstName";
    public const string LastName = "LastName";
    public const string Email = "Email";
    public const string ImageUrl = "ImageUrl";
    public const string Role = ClaimTypes.Role;
}
