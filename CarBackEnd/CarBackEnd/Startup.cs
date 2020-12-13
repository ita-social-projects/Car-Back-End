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
        public Startup(IConfiguration configuration, ILogger<Startup> logger, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
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
            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddDbContext(Configuration, Environment);
            services.AddControllers();
            services.AddLogging();
            services.AddApplicationInsightsTelemetry();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SoftServe Car-API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddCors(options => options.AddDefaultPolicy(
                builder => builder.AllowAnyOrigin()));
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

            app.UseRouting();

            app.UseCors();

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