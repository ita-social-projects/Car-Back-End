using System;
using System.Text;
using Car.Domain.Configurations;
using Car.Domain.Extensions;
using Car.Domain.Hubs;
using Car.WebApi.ServiceExtension;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

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
            services.AddServices(Configuration);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddCorsSettings();
            services.InitializeConfigurations(Configuration);
            services.AddLogging();
            if (Environment.IsProduction())
            {
                services.AddApplicationInsightsTelemetry();
            }

            services.AddSignalR();
            services.AddHangFire();
            services.AddHangfireServer();
            services.AddSwagger();

            // Configure this as suited for your case
            services.Configure<ForwardedHeadersOptions>(options =>
            {
              // Processing all forward headers (the default is None)
              options.ForwardedHeaders = ForwardedHeaders.All;

              // Clearing known networks and proxies collections
              options.KnownNetworks.Clear();
              options.KnownProxies.Clear();
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
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key!)),
                   };
               });

            services.AddMvc().AddFluentValidation();
            services.AddFluentValidators();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IServiceProvider serviceProvider,
            IRecurringJobManager recurringJobManager)
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

            app.UseForwardedHeaders();

            app.UseHttpMethodOverride();

            app.UseMiddlewareHandler();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SoftServe Car-API");
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/signalr");
            });

            app.UseHangfireDashboard();

            serviceProvider.AddReccuringJobs(recurringJobManager);
        }
    }
}
