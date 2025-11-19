using System.Reflection;
using FluentValidation;
using MediatR;
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
        
        services.AddValidatorsFromAssembly(typeof(CreateUser).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        
        
        
        return services;
    }
}