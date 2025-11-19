using Microsoft.Extensions.DependencyInjection;
using StudioSchedule.Application.UseCases.User;

namespace StudioSchedule.Application.Services;

public static class ServicesExtensions
{
    public static IServiceCollection ConfigureApplicationApp(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(CreateUser).Assembly
        ));
        
        return services;
    }
}