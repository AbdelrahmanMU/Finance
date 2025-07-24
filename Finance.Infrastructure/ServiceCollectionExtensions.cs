using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFinanceInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ApplicationDbContext>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        });

        return services;
    }
}
