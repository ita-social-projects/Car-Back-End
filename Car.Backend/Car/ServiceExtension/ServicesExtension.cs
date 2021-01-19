using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Data.Interfaces;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Implementation.Strategy;
using Car.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using File = Google.Apis.Drive.v3.Data.File;

namespace Car.ServiceExtension
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
            services.AddScoped<IImageService<Data.Entities.Car, File>, ImageService<Data.Entities.Car>>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IPreferencesService, PreferencesService>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IUnitOfWork<User>, UnitOfWork<User>>();
            services.AddScoped<IRepository<Data.Entities.Car>, Repository<Data.Entities.Car>>();
            services.AddScoped<IUnitOfWork<Data.Entities.Car>, UnitOfWork<Data.Entities.Car>>();
            services.AddScoped<IUnitOfWork<UserPreferences>, UnitOfWork<UserPreferences>>();
            services.AddScoped<IEntityTypeStrategy<User>, UserEntityStrategy>();
            services.AddScoped<IEntityTypeStrategy<Data.Entities.Car>, CarEntityStrategy>();
        }
    }
}
