using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Data.Interfaces;
using Car.Domain.Configurations;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Implementation.Strategy;
using Car.Domain.Services.Interfaces;
using Car.WebApi.JwtConfiguration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using File = Google.Apis.Drive.v3.Data.File;

namespace Car.WebApi.ServiceExtension
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
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IPreferencesService, PreferencesService>();

            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IUnitOfWork<User>, UnitOfWork<User>>();
            services.AddScoped<IRepository<Data.Entities.Car>, Repository<Data.Entities.Car>>();
            services.AddScoped<IUnitOfWork<Data.Entities.Car>, UnitOfWork<Data.Entities.Car>>();
            services.AddScoped<IRepository<UserPreferences>, Repository<UserPreferences>>();
            services.AddScoped<IUnitOfWork<UserPreferences>, UnitOfWork<UserPreferences>>();
            services.AddScoped<IRepository<Brand>, Repository<Brand>>();
            services.AddScoped<IUnitOfWork<Brand>, UnitOfWork<Brand>>();
            services.AddScoped<IRepository<Model>, Repository<Model>>();
            services.AddScoped<IUnitOfWork<Model>, UnitOfWork<Model>>();
            services.AddScoped<IEntityTypeStrategy<User>, UserEntityStrategy>();
            services.AddScoped<IEntityTypeStrategy<Data.Entities.Car>, CarEntityStrategy>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IUnitOfWork<Chat>, UnitOfWork<Chat>>();
            services.AddScoped<IJourneyService, JourneyService>();
            services.AddScoped<IRepository<Journey>, Repository<Journey>>();
            services.AddScoped<IUnitOfWork<Journey>, UnitOfWork<Journey>>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUnitOfWork<Notification>, UnitOfWork<Notification>>();
            services.AddScoped<IUnitOfWork<Message>, UnitOfWork<Message>>();
        }

        public static void InitializeConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Jwt>(configuration.GetSection("Jwt"));
            services.Configure<CredentialsFile>(configuration.GetSection("CredentialsFile"));
            services.Configure<GoogleFolders>(configuration.GetSection("GoogleFolders"));
            services.Configure<GoogleApplicationName>(configuration.GetSection("GoogleFolders"));
        }
    }
}