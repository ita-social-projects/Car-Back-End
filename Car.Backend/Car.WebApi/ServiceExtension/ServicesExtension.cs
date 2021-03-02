using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Configurations;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using File = Google.Apis.Drive.v3.Data.File;

namespace Car.WebApi.ServiceExtension
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICompressor, ImageCompressor>();
            services.AddScoped<IFileService<File>, GoogleDriveService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IPreferencesService, PreferencesService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IJourneyService, JourneyService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IWebTokenGenerator, JsonWebTokenGenerator>();

            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Data.Entities.Car>, Repository<Data.Entities.Car>>();
            services.AddScoped<IRepository<UserPreferences>, Repository<UserPreferences>>();
            services.AddScoped<IRepository<Brand>, Repository<Brand>>();
            services.AddScoped<IRepository<Model>, Repository<Model>>();
            services.AddScoped<IRepository<Chat>, Repository<Chat>>();
            services.AddScoped<IRepository<Message>, Repository<Message>>();
            services.AddScoped<IRepository<Journey>, Repository<Journey>>();
            services.AddScoped<IRepository<Notification>, Repository<Notification>>();
        }

        public static void InitializeConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Jwt>(configuration.GetSection("Jwt"));
            services.Configure<GoogleDriveOptions>(configuration.GetSection("GoogleDriveOptions"));
        }
    }
}