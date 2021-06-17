using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Car.Domain.Exceptions;
using Car.UnitTests.Base;
using Car.WebApi.Middelware;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        private readonly HttpContext context;

        public ExceptionMiddlewareTest()
        {
            env = new Mock<IWebHostEnvironment>();
            logger = new Mock<ILogger<ExceptionMiddleware>>();
            next = new Mock<RequestDelegate>();
            context = new DefaultHttpContext();
            exceptionMiddleware = new ExceptionMiddleware(next.Object, env.Object, logger.Object);
        }

        [Fact]
        public async Task InvokeAsync_WhenHttpInvoke_ExecuteOnce()
        {
            // Act
            await exceptionMiddleware.InvokeAsync(context);

            // Assert
            next.Verify(n => n.Invoke(context), Times.Once());
        }

        [Fact]
        public async Task InvokeAsync_WhenDbUpdateConcurrencyExceptionThrown_ReturnNoContentStatusCode()
        {
            // Arrange
            env.SetupGet(e => e.EnvironmentName).Returns("Development");
            next.Setup(n => n.Invoke(context)).Throws(new DbUpdateConcurrencyException());

            // Act
            await exceptionMiddleware.InvokeAsync(context);

            // Assert
            context.Response.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task InvokeAsync_WhenNotSuitableExceptionThrown_InternalServerErrorStatusCode()
        {
            // Arrange
            int expectedStatusCode = StatusCodes.Status500InternalServerError;
            Exception exception = new Fixture().Create<Exception>();
            env.SetupGet(e => e.EnvironmentName).Returns("Development");
            next.Setup(n => n.Invoke(context)).Throws(exception);

            // Act
            await exceptionMiddleware.InvokeAsync(context);

            // Assert
            context.Response.StatusCode.Should().Be(expectedStatusCode);
        }

        [Fact]
        public async Task InvokeAsync_WhenDbUpdateExceptionThrown_ReturnServerErrorCode()
        {
            // Arrange
            env.SetupGet(e => e.EnvironmentName).Returns("Development");
            next.Setup(n => n.Invoke(context)).Throws(new DbUpdateException());

            // Act
            await exceptionMiddleware.InvokeAsync(context);

            // Assert
            context.Response.StatusCode.Should().Be(500);
        }

        [Theory]
        [AutoData]
        public async Task InvokeAsync_WhenDefaultApplicationExceptionThrown_ReturnExceptionStatusCode(
            int exceptionStatusCode)
        {
            // Arrange
            env.SetupGet(e => e.EnvironmentName).Returns("Development");
            next.Setup(n => n.Invoke(context)).Throws(new DefaultApplicationException() { StatusCode = exceptionStatusCode });

            // Act
            await exceptionMiddleware.InvokeAsync(context);

            // Assert
            context.Response.StatusCode.Should().Be(exceptionStatusCode);
        }
    }
}
