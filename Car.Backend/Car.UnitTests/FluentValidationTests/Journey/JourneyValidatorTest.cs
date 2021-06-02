using System;
using AutoFixture.Xunit2;
using Car.Data.Constants;
using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;
using RangeAttr = System.ComponentModel.DataAnnotations.RangeAttribute;

namespace Car.UnitTests.FluentValidationTests.Journey
{
    [TestFixture]
    public class JourneyValidatorTest
    {
        private readonly JourneyValidator validator;

        public JourneyValidatorTest()
        {
            validator = new JourneyValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journey => journey.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.Id, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void RouteDistance_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journey => journey.RouteDistance, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void RouteDistance_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.RouteDistance, value);
        }

        [Xunit.Theory]
        [InlineData("2020-01-01")]
        public void DepartureTime_IsNotValid_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(journey => journey.DepartureTime, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData("2090-06-27")]
        public void DepartureTime__IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.DepartureTime, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(12)]
        public void CountOfSeats__IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journey => journey.CountOfSeats, value);
        }

        [Xunit.Theory]
        [AutoData]
        public void CountOfSeats_IsSpecified_NotGeneratesValidationError([RangeAttr(1, 4)] int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.CountOfSeats, value);
        }

        [Fact]
        public void Comments__IsNotValid_GeneratesValidationError()
        {
            string longCommnt = new string('*', Constants.CommentsMaxLength + 1);
            validator.ShouldHaveValidationErrorFor(journey => journey.Comments, longCommnt);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData("comment")]
        [InlineData(null)]
        public void Comments_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.Comments, value);
        }

        [Xunit.Theory]
        [AutoData]
        public void IsFree_IsSpecified_NotGeneratesValidationError(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.IsFree, value);
        }

        [Xunit.Theory]
        [AutoData]
        public void IsOnOwnCar_IsSpecified_NotGeneratesValidationError(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.IsOnOwnCar, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void OrganizerId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journey => journey.OrganizerId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void OrganizerId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.OrganizerId, value);
        }
    }
}
