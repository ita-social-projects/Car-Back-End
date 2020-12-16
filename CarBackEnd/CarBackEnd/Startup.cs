using System;
using System.IO;
using System.Reflection;
using Car.BLL.Dto.Email;
using Car.BLL.Services.Implementation;
using Car.BLL.Services.Interfaces;
using CarBackEnd.ServiceExtension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RazorClassLibraryForEmails.Services;

namespace CarBackEnd
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
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        private IWebHostEnvironment Environment { get; }

        private readonly ILogger _logger;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddSingleton<ISmptClient, SmtpClientService>();
            services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
            services.Configure<EmailConfiguration>(Configuration.GetSection(nameof(EmailConfiguration)));
            services.AddDbContext(Configuration);
            services.AddControllers();
            services.AddServices();
            services.AddCorsSettings();

            services.AddLogging();
            services.AddApplicationInsightsTelemetry();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SoftServe Car-API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                _logger.LogInformation("Configuring for Development environment");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                _logger.LogInformation("Configuring for Production environment");
            }

            app.UseMiddelwareHendler();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SoftServe Car-API");
            });
        }
    }
}