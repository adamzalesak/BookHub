﻿using BusinessLayer.Services;
using BusinessLayer.Services.Abstraction;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace WebMVC
{
    public static class Configuration
    {
        internal static IServiceCollection AddConfiguration(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextFactory<BookHubDbContext>(options => options.UseNpgsql(connectionString));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ICartsService, CartsService>();
            services.AddScoped<IPricesService, PricesService>();
            services.AddScoped<IOrdersService, OrdersService>();

            return services;
        }
    }
}
