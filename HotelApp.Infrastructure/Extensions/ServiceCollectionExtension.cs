using HotelApp.Infrastructure.Persistence;
using HotelApp.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HotelDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("HotelApp")));

            services.AddScoped<HotelRoomSeeder>();
        }
    }
}
