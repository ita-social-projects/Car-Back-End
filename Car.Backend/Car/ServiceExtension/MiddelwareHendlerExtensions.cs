using Car.Middelware;
using Microsoft.AspNetCore.Builder;

namespace Car.ServiceExtension
{
    public static class MiddelwareHendlerExtensions
    {
        public static IApplicationBuilder UseMiddelwareHendler(this IApplicationBuilder builder) =>
            builder.UseMiddleware<ExceptionMiddleware>();
    }
}
