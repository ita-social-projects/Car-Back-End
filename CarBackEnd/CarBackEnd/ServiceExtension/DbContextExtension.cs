using Car.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
