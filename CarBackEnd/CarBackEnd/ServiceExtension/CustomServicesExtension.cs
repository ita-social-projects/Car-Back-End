using Car.BLL.Services.Implementation;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Infrastructure;
using Car.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using File = Google.Apis.Drive.v3.Data.File;

namespace CarBackEnd.ServiceExtension
{
    public static class CustomServicesExtension
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ICompressor, CompressorWithQuality>();
            services.AddScoped<IDriveService<File>, GoogleDriveService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IUnitOfWork<User>, UnitOfWork<User>>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IRepository<Car.DAL.Entities.Car>, Repository<Car.DAL.Entities.Car>>();
            services.AddScoped<IUnitOfWork<Car.DAL.Entities.Car>, UnitOfWork<Car.DAL.Entities.Car>>();
        }
    }
}
