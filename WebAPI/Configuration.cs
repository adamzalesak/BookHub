using DataAccessLayer.Data;

namespace WebAPI;

public static class Configuration
{
    internal static IServiceCollection AddConfiguration(this IServiceCollection services)
    {

        services.AddDbContext<BookHubBdContext>();
        
        return services;
    }
}