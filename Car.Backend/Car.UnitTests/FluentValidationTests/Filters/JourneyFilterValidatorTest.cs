using System;
using AutoFixture.Xunit2;
using Car.Data.Constants;
using Car.Data.Enums;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;
using RangeAttr = System.ComponentModel.DataAnnotations.RangeAttribute;

namespace Car.UnitTests.FluentValidationTests.Filters
{
    [TestFixture]
    public class JourneyFilterValidatorTest
    {
        private readonly JourneyFilterValidator validator;

        public JourneyFilterValidatorTest()
        {
            validator = new JourneyFilterValidator();
        }

        [Xunit.Theory]
        [InlineData("2020-01-01")]
        public void DepartureTime_IsNotValid_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(filter => filter.DepartureTime, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData("2140-01-01")]
        public void DepartureTime_IsValid_GeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(filter => filter.DepartureTime, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [AutoData]
        public void PassengersCount_IsValid_NotGeneratesValidationError([RangeAttr(Constants.SeatsMinCount, Constants.SeatsMaxCount)]int value)
        {
            validator.ShouldNotHaveValidationErrorFor(filter => filter.PassengersCount, value);
        }

        [Xunit.Theory]
        [InlineData(-5)]
        [InlineData(100)]
        public void PassengersCount_IsNotValid_NotGeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(filter => filter.PassengersCount, value);
        }

        [Xunit.Theory]
        [InlineData(FeeType.All)]
        public void Fee_IsValid_NotGeneratesValidationError(FeeType value)
        {
            validator.ShouldNotHaveValidationErrorFor(filter => filter.Fee, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ApplicantId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(filter => filter.ApplicantId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void ApplicantId_IsValid_GeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(filter => filter.ApplicantId, value);
        }

        [Xunit.Theory]
        [AutoData]
        public void FromLatitude_IsValid_NotGeneratesValidationError([RangeAttr(Constants.MinLatitude, Constants.MaxLatitude)] int value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.ToLatitude, value);
        }

        [Xunit.Theory]
        [AutoData]
        public void FromLongitude_IsValid_NotGeneratesValidationError([RangeAttr(Constants.MinLongitude, Constants.MaxLongitude)] int value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.ToLongitude, value);
        }

        [Xunit.Theory]
        [AutoData]
        public void ToLatitude_IsValid_NotGeneratesValidationError([RangeAttr(Constants.MinLatitude, Constants.MaxLatitude)] int value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.FromLatitude, value);
        }

        [Xunit.Theory]
        [AutoData]
        public void ToLongitude_IsValid_NotGeneratesValidationError([RangeAttr(Constants.MinLongitude, Constants.MaxLongitude)] int value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.FromLongitude, value);
        }

        [Xunit.Theory]
        [InlineData(-200)]
        [InlineData(200)]
        public void FromLatitude_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(address => address.FromLatitude, value);
        }

        [Xunit.Theory]
        [InlineData(-200)]
        [InlineData(200)]
        public void FromLongitude_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(address => address.FromLongitude, value);
        }

        [Xunit.Theory]
        [InlineData(-200)]
        [InlineData(200)]
        public void ToLatitude_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(address => address.ToLatitude, value);
        }

        [Xunit.Theory]
        [InlineData(-200)]
        [InlineData(200)]
        public void ToLongitude_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(address => address.FromLongitude, value);
        }
    }
}
