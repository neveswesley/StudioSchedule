using Microsoft.Extensions.DependencyInjection;
using StudioSchedule.Application.UseCases.User;

namespace StudioSchedule.Application;

public static class ServicesExtensions
{
    public static IServiceCollection ConfigureApplicationApp(this IServiceCollection services)
    {
        AddMediatR(services);
        return services;
    }

    public static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(CreateUser).Assembly
        ));
    }
}