using System.Text;
using Car.Domain.Configurations;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Car.WebApi.ServiceExtension
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR();
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

            var jwtOptions = configuration.GetSection(nameof(Jwt)).Get<Jwt>();
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
        }
    }
}