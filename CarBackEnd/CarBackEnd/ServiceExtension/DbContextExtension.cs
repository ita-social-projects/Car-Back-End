using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Car.DAL.Context;

namespace CarBackEnd.ServiceExtension
{
    public static class DbContextExtension
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString;

            connectionString = configuration.GetConnectionString("CarConnection");

            services.AddDbContext<CarContext>(options =>
                  options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Car.DAL")));
        }
    }
}
