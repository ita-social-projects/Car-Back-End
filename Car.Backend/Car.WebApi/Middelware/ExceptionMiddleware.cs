using System;
using System.Collections.Generic;
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

            string responseMessage = "Internal server error";
            string logMessage = exception.Message;
            Dictionary<Type, ResponseInformation> exceptions = GenerateExceptionsDictionary(exception);

            if (env.IsDevelopment())
            {
                ResponseInformation responseInformation = new ResponseInformation();
                if (exceptions.TryGetValue(exception.GetType(), out responseInformation))
                {
                    logMessage = responseInformation.LogMessage;
                    responseMessage = responseInformation.ResponseMessage;
                    context.Response.ContentType = responseInformation.ContentType;
                    context.Response.StatusCode = responseInformation.StatusCode;
                }
            }

            logger.LogError(logMessage);
            return context.Response.WriteAsync(responseMessage);
        }

        private Dictionary<Type, ResponseInformation> GenerateExceptionsDictionary(Exception exception)
        {
            DefaultApplicationException defaultApplicationException = exception as DefaultApplicationException;
            return new Dictionary<Type, ResponseInformation>
            {
                {
                    typeof(DbUpdateConcurrencyException),
                    new ResponseInformation()
                    {
                        LogMessage = JsonConvert.SerializeObject(
                            new
                            {
                                StatusCode = 204,
                            }),
                        ResponseMessage = JsonConvert.SerializeObject(
                            new
                            {
                                StatusCode = 204,
                            }),
                        StatusCode = 204,
                    }
                },
                {
                    typeof(DbUpdateException),
                    new ResponseInformation()
                    {
                        LogMessage = JsonConvert.SerializeObject(
                            new
                            {
                                StatusCode = 500,
                                exception.InnerException?.Message,
                                Severity = Severity.Error,
                            }),
                        ResponseMessage = JsonConvert.SerializeObject(
                            new
                            {
                                StatusCode = 500,
                                exception.InnerException?.Message,
                                Severity = Severity.Error,
                            }),
                        StatusCode = 500,
                        ContentType = "application/json",
                    }
                },
                {
                    typeof(DefaultApplicationException),
                    new ResponseInformation()
                    {
                        LogMessage = JsonConvert.SerializeObject(
                            new
                            {
                                defaultApplicationException?.StatusCode,
                                defaultApplicationException?.Message,
                                defaultApplicationException?.Severity,
                            }),
                        ResponseMessage = JsonConvert.SerializeObject(
                            new
                            {
                                defaultApplicationException?.StatusCode,
                                defaultApplicationException?.Message,
                                defaultApplicationException?.Severity,
                            }),
                        StatusCode = (exception is DefaultApplicationException ex) ? ex.StatusCode : 200,
                        ContentType = "application/json",
                    }
                },
            };
        }
    }
}