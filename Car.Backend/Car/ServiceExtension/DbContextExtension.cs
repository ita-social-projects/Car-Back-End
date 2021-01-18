using Car.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Car.ServiceExtension
{
    public static class DbContextExtension
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("CarConnection");

            services.AddDbContext<CarContext>(options =>
                  options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Car.DAL")));
        }
    }
}
