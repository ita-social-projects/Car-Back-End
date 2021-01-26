using Car.WebApi.Middelware;
using Microsoft.AspNetCore.Builder;

namespace Car.WebApi.ServiceExtension
{
    public static class MiddelwareHendlerExtensions
    {
        public static IApplicationBuilder UseMiddelwareHendler(this IApplicationBuilder builder) =>
            builder.UseMiddleware<ExceptionMiddleware>();
    }
}
