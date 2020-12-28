﻿using System;
using System.Threading.Tasks;
using Car.BLL.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarBackEnd.Middelware
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

                    string responseContent = JsonConvert.SerializeObject(
                        new
                        {
                            appException.StatusCode,
                            appException.Message,
                            appException.Severity,
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
