using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Core.Entities;

namespace OnlineShop.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEfCoreAndIdentity(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("MySQL");
            var mySqlVersion = new MySqlServerVersion(Version.Parse(config["DbProviders:MySQL:Version"]));
            
            services
                .AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, mySqlVersion))
                .AddIdentity<AppUser, IdentityRole>()  
                .AddEntityFrameworkStores<AppDbContext>();
            return services;
        }
    }
}