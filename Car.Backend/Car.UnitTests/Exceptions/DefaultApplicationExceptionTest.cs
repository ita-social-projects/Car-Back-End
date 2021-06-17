using System;
using System.Runtime.Serialization;
using Car.Domain.Dto;
using Car.Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace Car.UnitTests.Exceptions
{
    public class DefaultApplicationExceptionTest : DefaultApplicationException
    {
        private static readonly SerializationInfo SerializationInfo =
            new SerializationInfo(Type.GetType(It.IsAny<string>())!, new FormatterConverter());

        private readonly DefaultApplicationException defaultApplicationException = new DefaultApplicationException();
        private readonly string message = It.IsAny<int>().ToString();

        public DefaultApplicationExceptionTest()
        {
        }

        [Fact]
        public void TestParameterLessConstructor()
        {
            Action action = () => throw new DefaultApplicationException();

            action.Should().Throw<DefaultApplicationException>();
        }

        [Fact]
        public void TestConstructor()
        {
            Action action = () => throw new DefaultApplicationException(message);

            action.Should().Throw<DefaultApplicationException>().WithMessage(message);
        }

        [Fact]
        public void TestProtectedConstructor()
        {
            Action action = () => throw new DefaultApplicationExceptionTest();

            action.Should().Throw<DefaultApplicationException>();
        }

        [Fact]
        public void TestGetObjectData()
        {
            Action action = () => defaultApplicationException.GetObjectData(SerializationInfo, default);

            action.Should().BeOfType<Action>();
        }

        [Fact]
        public void TestStatusCode()
        {
            defaultApplicationException.StatusCode = 200;

            defaultApplicationException.StatusCode.Should().Be(200);
        }

        [Fact]
        public void TestSeverity()
        {
            defaultApplicationException.Severity = Severity.Warning;

            defaultApplicationException.Severity.Should().Be(Severity.Warning);
        }
    }
}
