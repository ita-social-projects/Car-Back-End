using Microsoft.Extensions.DependencyInjection;

namespace CarBackEnd.ServiceExtension
{
    public static class CorsExtension
    {
        public static void AddCorsSettings(this IServiceCollection services)
        {
            services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy", builder => builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Token-Expired", "InvalidRefreshToken", "InvalidCredentials")
                        .WithOrigins("http://localhost:3000")
                        .AllowCredentials()
                        .Build());
                });
        }
    }
}
