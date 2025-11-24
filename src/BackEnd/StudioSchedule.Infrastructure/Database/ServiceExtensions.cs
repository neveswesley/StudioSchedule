using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using StudioSchedule.Domain.Interfaces;
using StudioSchedule.Infrastructure.Repositories;

namespace StudioSchedule.Infrastructure.Database;

public static class ServiceExtensions
{
    public static void ConfigurePersistenceApp(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services);
        AddRepositories(services);
    }

    private static void AddDbContext(IServiceCollection services)
    {
        var connectionString =
            "Server=WESLEY\\SQLEXPRESS;Database=StudioSchedule.Db;User ID=sa;Password=1q2w3e4r5t@#;TrustServerCertificate=True;";

        services.AddDbContext<AppDbContext>(dbContextOptions => { dbContextOptions.UseSqlServer(connectionString); });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IStudioRepository, StudioRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}