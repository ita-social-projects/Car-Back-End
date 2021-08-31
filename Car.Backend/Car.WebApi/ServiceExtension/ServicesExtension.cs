using Azure.Storage.Blobs;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Configurations;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Car.WebApi.ServiceExtension
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<BlobServiceClient>(provider =>
                new BlobServiceClient(configuration.GetSection("AzureBlobStorageOptions")
                    .GetValue<string>("AccessKey")));
            services.AddScoped<ICompressor, ImageCompressor>();
            services.AddSingleton<IFirebaseService, FirebaseService>();
            services.AddScoped<IPushNotificationService, PushNotificationService>();
            services.AddScoped<IFileService, AzureBlobStorageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IUserPreferencesService, UserPreferencesService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IJourneyService, JourneyService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IWebTokenGenerator, JsonWebTokenGenerator>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<ILocationTypeService, LocationTypeService>();
            services.AddScoped<IJourneyUserService, JourneyUserService>();

            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Data.Entities.Car>, Repository<Data.Entities.Car>>();
            services.AddScoped<IRepository<UserPreferences>, Repository<UserPreferences>>();
            services.AddScoped<IRepository<Brand>, Repository<Brand>>();
            services.AddScoped<IRepository<Model>, Repository<Model>>();
            services.AddScoped<IRepository<Chat>, Repository<Chat>>();
            services.AddScoped<IRepository<Message>, Repository<Message>>();
            services.AddScoped<IRepository<Journey>, Repository<Journey>>();
            services.AddScoped<IRepository<Notification>, Repository<Notification>>();
            services.AddScoped<IRepository<Location>, Repository<Location>>();
            services.AddScoped<IRepository<LocationType>, Repository<LocationType>>();
            services.AddScoped<IRepository<Request>, Repository<Request>>();
            services.AddScoped<IRepository<Stop>, Repository<Stop>>();
            services.AddScoped<IRepository<JourneyPoint>, Repository<JourneyPoint>>();
            services.AddScoped<IRepository<JourneyUser>, Repository<JourneyUser>>();
            services.AddScoped<IRepository<ReceivedMessages>, Repository<ReceivedMessages>>();
            services.AddScoped<IRepository<Schedule>, Repository<Schedule>>();
        }

        public static void InitializeConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Jwt>(configuration.GetSection("Jwt"));
            services.Configure<AzureBlobStorageOptions>(configuration.GetSection("AzureBlobStorageOptions"));
            services.Configure<FirebaseOptions>(configuration.GetSection("FirebaseOptions"));
        }
    }
}