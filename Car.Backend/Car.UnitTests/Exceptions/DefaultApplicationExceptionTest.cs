using System;
using Car.Domain.Dto;
using Car.Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace Car.UnitTests.Exceptions
{
    public class DefaultApplicationExceptionTest
    {
        private readonly DefaultApplicationException _defaultApplicationException = new DefaultApplicationException();

        [Fact]
        public void TestParameterLessConstructor()
        {
            Action action = () => throw new DefaultApplicationException();

            action.Should().Throw<DefaultApplicationException>();
        }

        [Fact]
        public void TestConstructor()
        {
            Action action = () => throw new DefaultApplicationException("test");

            action.Should().Throw<DefaultApplicationException>().WithMessage("test");
        }

        [Fact]
        public void StatusCodeTest()
        {
            _defaultApplicationException.StatusCode = 200;

            _defaultApplicationException.StatusCode.Should().Be(200);
        }

        [Fact]
        public void SeverityTest()
        {
            _defaultApplicationException.Severity = Severity.Warning;

            _defaultApplicationException.Severity.Should().Be(Severity.Warning);
        }
    }
}
