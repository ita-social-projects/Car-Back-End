using Car.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Car.WebApi.ServiceExtension
{
    public static class DbContextExtension
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("CarConnection");

            services.AddDbContext<CarContext>(options =>
                  options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Car.Data")));
        }
    }
}
