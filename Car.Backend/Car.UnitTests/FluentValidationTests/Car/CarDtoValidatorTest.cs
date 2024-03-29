﻿using Car.Data.Constants;
using Car.Data.Enums;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Car
{
    public class CarDtoValidatorTest
    {
        private readonly CarDtoValidator validator;

        public CarDtoValidatorTest()
        {
            validator = new CarDtoValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(carDto => carDto.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(carDto => carDto.Id, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void OwnerId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(carDto => carDto.OwnerId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void OwnerId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(carDto => carDto.OwnerId, value);
        }

        [Xunit.Theory]
        [InlineData(Color.Black)]
        public void Color_IsSpecified_NotGeneratesValidationError(Color value)
        {
            validator.ShouldNotHaveValidationErrorFor(carDto => carDto.Color, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData("12345678910")]
        public void PlateNumber_IsNotValid_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(carDto => carDto.PlateNumber, value);
        }

        [Fact]
        public void PlateNumber_IsTooLong_GeneratesValidationError()
        {
            string longCommnt = new string('*', Constants.PlateNumberMaxLength + 1);
            validator.ShouldHaveValidationErrorFor(carDto => carDto.PlateNumber, longCommnt);
        }

        [Xunit.Theory]
        [InlineData("12345")]
        [InlineData(null)]
        public void PlateNumber_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(carDto => carDto.PlateNumber, value);
        }
    }
}
