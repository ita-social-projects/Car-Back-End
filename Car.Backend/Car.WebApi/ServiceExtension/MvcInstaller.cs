using System.Text;
using Car.Domain.Configurations;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;

namespace Car.WebApi.ServiceExtension
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR()
                .AddAzureSignalR(
                    configuration.GetSection("AzureSignalRService")
                        .GetValue<string>("ConnectionString"));
            services.AddHangfireServer();

            // Configure this as suited for your case
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                // Processing all forward headers (the default is None)
                options.ForwardedHeaders = ForwardedHeaders.All;

                // Clearing known networks and proxies collections
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(configuration);

            services.AddMvc().AddFluentValidation();
        }
    }
}