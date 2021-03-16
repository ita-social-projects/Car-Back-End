using System;
using Car.Data;
using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
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
        [InlineData(1)]
        [InlineData(8)]
        public void CountOfSeats_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.CountOfSeats, value);
        }

        [Fact]
        public void Comments__IsNotValid_GeneratesValidationError()
        {
            string longCommnt = new string('*', Constants.COMMENTS_MAX_LENGTH + 1);
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
        [InlineData(true)]
        [InlineData(false)]
        public void IsFree__IsSpecified_NotGeneratesValidationError(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.IsFree, value);
        }
    }
}
