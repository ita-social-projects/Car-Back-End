using System;
using Azure.Identity;
using Azure.Storage.Blobs;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Configurations;
using Car.Domain.Extensions;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Chat = Car.Data.Entities.Chat;
using Invitation = Car.Data.Entities.Invitation;
using Location = Car.Data.Entities.Location;
using LocationType = Car.Data.Entities.LocationType;
using Message = Car.Data.Entities.Message;
using Schedule = Car.Data.Entities.Schedule;
using User = Car.Data.Entities.User;

namespace Car.WebApi.ServiceExtension
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped(_ =>
                new BlobServiceClient(configuration.GetSection("AzureBlobStorageOptions")
                    .GetValue<string>("AccessKey")));
            services.AddScoped(_ =>
                new GraphServiceClient(
                    new ClientSecretCredential(
                        configuration.GetSection("AzureAd").GetValue<string>("TenantId"),
                        configuration.GetSection("AzureAd").GetValue<string>("ClientId"),
                        configuration.GetSection("AzureAd").GetValue<string>("ClientSecret"))));
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
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<ILocationTypeService, LocationTypeService>();
            services.AddScoped<IJourneyUserService, JourneyUserService>();
            services.AddScoped<IReceivedMessagesService, ReceivedMessagesService>();

            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Data.Entities.Car>, Repository<Data.Entities.Car>>();
            services.AddScoped<IRepository<UserPreferences>, Repository<UserPreferences>>();
            services.AddScoped<IRepository<Brand>, Repository<Brand>>();
            services.AddScoped<IRepository<Model>, Repository<Model>>();
            services.AddScoped<IRepository<Chat>, Repository<Chat>>();
            services.AddScoped<IRepository<FcmToken>, Repository<FcmToken>>();
            services.AddScoped<IRepository<Message>, Repository<Message>>();
            services.AddScoped<IRepository<Journey>, Repository<Journey>>();
            services.AddScoped<IRepository<Invitation>, Repository<Invitation>>();
            services.AddScoped<IRepository<Notification>, Repository<Notification>>();
            services.AddScoped<IRepository<Location>, Repository<Location>>();
            services.AddScoped<IRepository<LocationType>, Repository<LocationType>>();
            services.AddScoped<IRepository<Request>, Repository<Request>>();
            services.AddScoped<IRepository<Stop>, Repository<Stop>>();
            services.AddScoped<IRepository<JourneyPoint>, Repository<JourneyPoint>>();
            services.AddScoped<IRepository<JourneyUser>, Repository<JourneyUser>>();
            services.AddScoped<IRepository<ReceivedMessages>, Repository<ReceivedMessages>>();
            services.AddScoped<IRepository<Schedule>, Repository<Schedule>>();

            services.Configure<AzureBlobStorageOptions>(configuration.GetSection("AzureBlobStorageOptions"));
            services.Configure<FirebaseOptions>(configuration.GetSection("FirebaseOptions"));
        }
    }
}