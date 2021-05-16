using System;
using System.Collections.Generic;
using Car.Domain.Dto;
using Car.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Car.WebApi.Middelware
{
    public static class DbExceptions
    {
        public static Dictionary<Type, ResponseInformation> GetExceptions(Exception exception)
        {
            var exceptionsDictionary = new Dictionary<Type, ResponseInformation>();
            var defaultApplicationException = exception as DefaultApplicationException;

            exceptionsDictionary.Add(
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
                });

            exceptionsDictionary.Add(
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
                });

            exceptionsDictionary.Add(
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
                });

            return exceptionsDictionary;
        }
    }
}
