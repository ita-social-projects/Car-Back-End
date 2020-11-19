using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Car.DAL.Context;


namespace CarBackEnd.ServiceExtension
{
    public static class ServiceExtension
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            string connectionString;

            if (!env.IsProduction())
                connectionString = configuration.GetConnectionString("CarConnection");
            else
                connectionString = configuration.GetConnectionString("AzureConnection");

            services.AddDbContext<CarContext>(options =>
                  options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Car.DAL")));
        }
    }
}
