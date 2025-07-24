using Finance.Domain;
using Microsoft.AspNetCore.Identity;

namespace Finance.Infrastructure;

public static class ApplicationDbContextSeed
{
    public static async Task SeedDataAsync(RoleManager<IdentityRole> roleManager)
    {
        if (roleManager.Roles.Any())
            return;

        var names = Enum.GetNames(typeof(Roles));

        foreach (var roleName in names)
        {
            var identityRole = new IdentityRole(roleName);
            await roleManager.CreateAsync(identityRole);
        }
    }
}
