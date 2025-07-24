using Finance.Domain;
using Finance.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Financ.API.Helpers;

public static class DatabaseExtension
{
    public static async Task MigrateDatabase(this IServiceScope scope)
    {
        var projectDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await projectDbContext.Database.MigrateAsync();
    }

    public static async Task SeedDatabase(this IServiceScope scope)
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await ApplicationDbContextSeed.SeedDataAsync(roleManager);
    }
}
