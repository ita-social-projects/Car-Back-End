using System;
using System.Threading.Tasks;
using Car.BLL.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;

namespace CarBackEnd.Middelware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
               await _next(httpContext);
            }
            catch (Exception ex)
            {
               await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (_env.IsDevelopment())
            {
                if (exception is DefaultApplicationException appException)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = appException.StatusCode;
                    return context.Response.WriteAsync(JsonConvert.SerializeObject(new
                    {
                        appException.StatusCode,
                        appException.Message,
                        appException.Severity,
                    }));
                }

                return context.Response.WriteAsync(JsonConvert.SerializeObject(exception));
            }

            return context.Response.WriteAsync("Internal server error");
        }
    }
}
