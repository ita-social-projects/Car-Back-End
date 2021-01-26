using Car.Domain.Dto;
using Car.Domain.Exceptions;
using Car.WebApi.Middelware;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Car.UnitTests
{
    public class ExceptionMiddlewareTest
    {
        private readonly Mock<ExceptionMiddleware> exceptionMiddleware;

        public ExceptionMiddlewareTest()
        {
            exceptionMiddleware = new Mock<ExceptionMiddleware>();
        }

        [Fact]
        public void TestHandleExceptionAsync()
        {
            var ex = new DefaultApplicationException(It.IsAny<string>())
            {
                StatusCode = It.IsAny<int>(),
                Severity = It.IsAny<Severity>(),
            };

            var context = new DefaultHttpContext();
        }
    }
}
