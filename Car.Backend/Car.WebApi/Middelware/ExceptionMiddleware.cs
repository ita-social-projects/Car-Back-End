using System;
using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Car.WebApi.Middelware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment env;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            IWebHostEnvironment env,
            ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.env = env;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
               await next(httpContext);
            }
            catch (Exception ex)
            {
               await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            logger.LogInformation("Handle error!");
            if (env.IsDevelopment())
            {
                if (exception is DefaultApplicationException appException)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = appException.StatusCode;

                    var responseContent = JsonConvert.SerializeObject(
                        new
                        {
                            appException.StatusCode,
                            appException.Message,
                            appException.Severity,
                        });

                    logger.LogError(responseContent);
                    return context.Response.WriteAsync(responseContent);
                }

                if (exception is DbUpdateConcurrencyException)
                {
                    context.Response.StatusCode = 204;

                    var responseContent = JsonConvert.SerializeObject(
                        new
                        {
                            StatusCode = 204,
                        });

                    return context.Response.WriteAsync(responseContent);
                }

                if (exception is DbUpdateException)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 500;

                    var responseContent = JsonConvert.SerializeObject(
                        new
                        {
                            StatusCode = 500,
                            Message = exception.InnerException.Message,
                            Severity = Severity.Error,
                        });

                    logger.LogError(responseContent);
                    return context.Response.WriteAsync(responseContent);
                }

                logger.LogError(exception.Message);
                return context.Response.WriteAsync(JsonConvert.SerializeObject(exception));
            }

            logger.LogError(exception.Message);
            return context.Response.WriteAsync("Internal server error");
        }
    }
}
