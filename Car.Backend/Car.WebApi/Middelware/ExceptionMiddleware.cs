using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

        private static Dictionary<Type, ResponseInformation> GenerateExceptionsDictionary(Exception exception)
        {
            var exceptionDictionary = new Dictionary<Type, ResponseInformation>();

            var result = exceptionDictionary
            .Union(DbExceptions.GetExceptions(exception))
            .ToDictionary(k => k.Key, v => v.Value);

            return result;
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            logger.LogInformation("Handle error!");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            string responseMessage = "Internal server error";
            string logMessage = exception.Message;

            Dictionary<Type, ResponseInformation> exceptions = GenerateExceptionsDictionary(exception);

            if (exceptions.TryGetValue(exception.GetType(), out ResponseInformation? responseInformation))
            {
                logMessage = responseInformation.LogMessage;

                context.Response.ContentType = responseInformation.ContentType;
                context.Response.StatusCode = responseInformation.StatusCode;
                if (env.IsDevelopment())
                {
                    responseMessage = responseInformation.ResponseMessage;
                }
            }

            logger.LogError(logMessage);
            return context.Response.WriteAsync(responseMessage);
        }
    }
}