using Car.WebApi.Middelware;
using Microsoft.AspNetCore.Builder;

namespace Car.WebApi.ServiceExtension
{
    public static class MiddlewareHandlerExtensions
    {
        public static IApplicationBuilder UseMiddlewareHandler(this IApplicationBuilder builder) =>
            builder.UseMiddleware<ExceptionMiddleware>();
    }
}
