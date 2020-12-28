using CarBackEnd.Middelware;
using Microsoft.AspNetCore.Builder;

namespace CarBackEnd.ServiceExtension
{
    public static class MiddelwareHendlerExtensions
    {
        public static IApplicationBuilder UseMiddelwareHendler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
