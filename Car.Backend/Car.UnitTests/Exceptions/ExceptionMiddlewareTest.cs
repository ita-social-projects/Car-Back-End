using System;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Context;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.UnitTests.Base;
using Car.WebApi.Middelware;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Car.UnitTests.Exceptions
{
    public class ExceptionMiddlewareTest : TestBase
    {
        private readonly ExceptionMiddleware exceptionMiddleware;
        private readonly Mock<IWebHostEnvironment> env;
        private readonly Mock<ILogger<ExceptionMiddleware>> logger;
        private readonly Mock<RequestDelegate> next;
        private readonly Mock<HttpContext> context;

        public ExceptionMiddlewareTest()
        {
            env = new Mock<IWebHostEnvironment>();
            logger = new Mock<ILogger<ExceptionMiddleware>>();
            next = new Mock<RequestDelegate>();
            context = new Mock<HttpContext>();
            exceptionMiddleware = new ExceptionMiddleware(next.Object, env.Object, logger.Object);
        }

        [Fact]
        public async Task InvokeAsync_WhenHttpInvoke_ExecuteOnce()
        {
            // Act
            await exceptionMiddleware.InvokeAsync(context.Object);

            // Assert
            next.Verify(n => n.Invoke(context.Object), Times.Once());
        }
    }
}
