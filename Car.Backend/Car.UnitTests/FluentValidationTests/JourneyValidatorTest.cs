using System;
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
        public void Should_have_error_when_Id_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(journey => journey.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_Id_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.Id, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_have_error_when_RouteDistance_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(journey => journey.RouteDistance, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_RouteDistance_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.RouteDistance, value);
        }

        [Xunit.Theory]
        [InlineData("2020-01-01")]
        public void Should_have_error_when_DepartureTime_is_not_valid(string value)
        {
            validator.ShouldHaveValidationErrorFor(journey => journey.DepartureTime, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData("2090-06-27")]
        public void Should_not_have_error_when_DepartureTime_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.DepartureTime, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(12)]
        public void Should_have_error_when_CountOfSeats_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(journey => journey.CountOfSeats, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(9)]
        public void Should_not_have_error_when_CountOfSeats_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.CountOfSeats, value);
        }

        [Fact]
        public void Should_have_error_when_Comments_is_longer_than_101()
        {
            string longCommnt = new string('*', 101);
            validator.ShouldHaveValidationErrorFor(journey => journey.Comments, longCommnt);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData("comment")]
        [InlineData(null)]
        public void Should_not_have_error_when_Comments_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.Comments, value);
        }

        [Xunit.Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Should_not_have_error_when_IsFree_is_specified(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.IsFree, value);
        }
    }
}
