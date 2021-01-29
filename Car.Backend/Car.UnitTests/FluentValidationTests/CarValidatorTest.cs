﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    [TestFixture]
    public class CarValidatorTest
    {
        private CarValidator validator;

        public CarValidatorTest()
        {
            validator = new CarValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_have_error_when_Id_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(car => car.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_Id_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(car => car.Id, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData("a")]
        public void Should_have_error_when_Color_is_not_valid(string value)
        {
            validator.ShouldHaveValidationErrorFor(car => car.Color, value);
        }

        [Fact]
        public void Should_have_error_when_Color_is_too_long()
        {
            string longColor = new string('*', 101);
            validator.ShouldHaveValidationErrorFor(car => car.Color, longColor);
        }

        [Xunit.Theory]
        [InlineData("ColorName")]
        public void Should_not_have_error_when_Color_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(car => car.Color, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData("a")]
        public void Should_have_error_when_PlateNumber_is_not_valid(string value)
        {
            validator.ShouldHaveValidationErrorFor(car => car.Color, value);
        }

        [Xunit.Theory]
        [InlineData("Number1")]
        public void Should_not_have_error_when_PlateNumber_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(car => car.Color, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_have_error_when_UserId_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(car => car.UserId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_UserId_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(car => car.UserId, value);
        }
    }
}