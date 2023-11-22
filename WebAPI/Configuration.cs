using BusinessLayer.Services;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPI;

public static class Configuration
{
    internal static IServiceCollection AddConfiguration(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextFactory<BookHubBdContext>(options => options.UseNpgsql(connectionString));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddScoped<IBooksService, BooksService>();

        return services;
    }
}