using System;
using System.Collections.Generic;
using Car.Data.Entities;
using Car.Data.Enums;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Request
{
    [TestFixture]
    public class RequestDtoValidatorTest
    {
        private readonly RequestDtoValidator validator;

        public RequestDtoValidatorTest()
        {
            validator = new RequestDtoValidator();
        }

        [Xunit.Theory]
        [InlineData("2020-01-01")]
        public void DepartureTime_IsNotValid_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(request => request.DepartureTime, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData("2140-01-01")]
        public void DepartureTime_IsValid_GeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(request => request.DepartureTime, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData(3)]
        [InlineData(1)]
        public void PassengersCount_IsValid_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(request => request.PassengersCount, value);
        }

        [Xunit.Theory]
        [InlineData(-5)]
        [InlineData(100)]
        public void PassengersCount_IsNotValid_NotGeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(request => request.PassengersCount, value);
        }

        [Xunit.Theory]
        [InlineData(FeeType.All)]
        public void Fee_IsValid_NotGeneratesValidationError(FeeType value)
        {
            validator.ShouldNotHaveValidationErrorFor(request => request.Fee, value);
        }

        [Xunit.Theory]
        [InlineData(-3)]
        [InlineData(0)]
        public void UserId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(request => request.UserId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(4)]
        public void UserId_IsValid_GeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(request => request.UserId, value);
        }
    }
}
