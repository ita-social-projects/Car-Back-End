using System.Net.Http.Headers;
using Car.BLL.Dto;
using Car.BLL.Exceptions;
using CarBackEnd.Middelware;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Car.Tests
{
    public class ExceptionMiddlewareTest
    {
        private readonly Mock<ExceptionMiddleware> _exceptionMiddleware;

        public ExceptionMiddlewareTest()
        {
            _exceptionMiddleware = new Mock<ExceptionMiddleware>();
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
