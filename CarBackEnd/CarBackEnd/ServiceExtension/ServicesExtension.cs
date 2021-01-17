using Car.BLL.Services.Implementation;
using Car.BLL.Services.Implementation.Strategy;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Infrastructure;
using Car.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using File = Google.Apis.Drive.v3.Data.File;

namespace CarBackEnd.ServiceExtension
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICompressor, CompressorWithQuality>();
            services.AddScoped<IDriveService<File>, GoogleDriveService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IImageService<User, File>, ImageService<User>>();
            services.AddScoped<IImageService<Car.DAL.Entities.Car, File>, ImageService<Car.DAL.Entities.Car>>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IPreferencesService, PreferencesService>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IUnitOfWork<User>, UnitOfWork<User>>();
            services.AddScoped<IRepository<Car.DAL.Entities.Car>, Repository<Car.DAL.Entities.Car>>();
            services.AddScoped<IUnitOfWork<Car.DAL.Entities.Car>, UnitOfWork<Car.DAL.Entities.Car>>();
            services.AddScoped<IUnitOfWork<UserPreferences>, UnitOfWork<UserPreferences>>();
            services.AddScoped<IEntityTypeStrategy<User>, UserEntityStrategy>();
            services.AddScoped<IEntityTypeStrategy<Car.DAL.Entities.Car>, CarEntityStrategy>();
        }
    }
}
