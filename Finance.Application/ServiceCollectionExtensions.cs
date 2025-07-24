using Finance.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Finance.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFinanceApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped<IStorageService, StorageService>();

        return services;
    }
}
