using System;
using System.Threading.Tasks;
using Car.BLL.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CarBackEnd.Middelware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddelwareHendler
    {
        private readonly RequestDelegate _next;

        public CustomMiddelwareHendler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
               await _next(httpContext);
            }
            catch (Exception ex)
            {
               await HandleException(httpContext, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            if (exception is ApplicationCustomException customException)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = customException.StatusCode;
                return context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    customException.StatusCode,
                    customException.Message,
                    customException.Severity,
                }));
            }

            return context.Response.WriteAsync("Something went wrong");
        }
    }
}
