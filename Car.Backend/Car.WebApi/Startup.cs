using System;
using System.IO;
using System.Reflection;
using System.Text;
using Car.Data.Entities;
using Car.Data.FluentValidation;
using Car.Domain.Configurations;
using Car.Domain.Dto;
using Car.Domain.FluentValidationDto;
using Car.WebApi.Hubs;
using Car.WebApi.ServiceExtension;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Car.WebApi
{
    public class Startup
    {
        public Startup(ILogger<Startup> logger, IWebHostEnvironment environment)
        {
            Environment = environment;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            this.logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IConfiguration Configuration { get; }

        private IWebHostEnvironment Environment { get; }

        private readonly ILogger logger;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext(Configuration);
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddServices();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddCorsSettings();
            services.InitializeConfigurations(Configuration);
            services.AddLogging();
            services.AddApplicationInsightsTelemetry();

            services.AddSignalR();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SoftServe Car-API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            var jwtOptions = Configuration.GetSection(nameof(Jwt)).Get<Jwt>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = jwtOptions.Issuer,
                   ValidAudience = jwtOptions.Issuer,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
               };
           });

            services.AddMvc().AddFluentValidation();

            services.AddTransient<IValidator<Address>, AddressValidator>();
            services.AddTransient<IValidator<Brand>, BrandValidator>();
            services.AddTransient<IValidator<Data.Entities.Car>, CarValidator>();
            services.AddTransient<IValidator<Chat>, ChatValidator>();
            services.AddTransient<IValidator<Journey>, JourneyValidator>();
            services.AddTransient<IValidator<LocationType>, LocationTypeValidator>();
            services.AddTransient<IValidator<Location>, LocationValidator>();
            services.AddTransient<IValidator<Message>, MessageValidator>();
            services.AddTransient<IValidator<Model>, ModelValidator>();
            services.AddTransient<IValidator<Notification>, NotificationValidator>();
            services.AddTransient<IValidator<Schedule>, ScheduleValidator>();
            services.AddTransient<IValidator<Stop>, StopValidator>();
            services.AddTransient<IValidator<UserPreferences>, UserPreferencesValidator>();
            services.AddTransient<IValidator<User>, UserValidator>();

            services.AddTransient<IValidator<AddressDto>, AddressDtoValidator>();
            services.AddTransient<IValidator<CarDto>, CarDtoValidator>();
            services.AddTransient<IValidator<NotificationDto>, NotificationDtoValidator>();
            services.AddTransient<IValidator<ParticipantDto>, ParticipantDtoValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                logger.LogInformation("Configuring for Development environment");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                logger.LogInformation("Configuring for Production environment");
            }

            app.UseMiddlewareHandler();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/signalr");
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SoftServe Car-API");
            });
        }
    }
}