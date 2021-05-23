using System;
using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Request
{
    [TestFixture]
    public class RequestValidatorTest
    {
        private readonly RequestValidator validator;

        public RequestValidatorTest()
        {
            validator = new RequestValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(request => request.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void Id_IsValid_GeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(request => request.Id, value);
        }

        [Xunit.Theory]
        [InlineData("2020-01-01")]
        public void DepartureTime_IsNotValid_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorForAsync(request => request.DepartureTime, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void PassengersCount_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(request => request.PassengersCount, value);
        }
    }
}
