using CarBackEnd.Middelware;
using Microsoft.AspNetCore.Builder;

namespace CarBackEnd.ServiceExtension
{
    public static class CustomMiddelwareHendlerExtensions
    {
        public static IApplicationBuilder UseCustomMiddelwareHendler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddelwareHendler>();
        }
    }
}
