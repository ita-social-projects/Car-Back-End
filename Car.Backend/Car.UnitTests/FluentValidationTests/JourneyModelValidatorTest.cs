using System;
using Car.Data;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    [TestFixture]
    public class JourneyModelValidatorTest
    {
        private readonly JourneyModelValidator validator;

        public JourneyModelValidatorTest()
        {
            validator = new JourneyModelValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyModel => journeyModel.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyModel => journeyModel.Id, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void RouteDistance_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyModel => journeyModel.RouteDistance, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void RouteDistance_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyModel => journeyModel.RouteDistance, value);
        }

        [Xunit.Theory]
        [InlineData("2020-01-01")]
        public void DepartureTime_IsNotValid_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(journeyModel => journeyModel.DepartureTime, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData("2090-06-27")]
        public void DepartureTime__IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyModel => journeyModel.DepartureTime, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(12)]
        public void CountOfSeats__IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyModel => journeyModel.CountOfSeats, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(8)]
        public void CountOfSeats_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyModel => journeyModel.CountOfSeats, value);
        }

        [Fact]
        public void Comments__IsNotValid_GeneratesValidationError()
        {
            string longCommnt = new string('*', Constants.COMMENTS_MAX_LENGTH + 1);
            validator.ShouldHaveValidationErrorFor(journeyModel => journeyModel.Comments, longCommnt);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData("comment")]
        [InlineData(null)]
        public void Comments_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyModel => journeyModel.Comments, value);
        }

        [Xunit.Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsFree__IsSpecified_NotGeneratesValidationError(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyModel => journeyModel.IsFree, value);
        }

        [Xunit.Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsOnOwnCar__IsSpecified_NotGeneratesValidationError(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyModel => journeyModel.IsOnOwnCar, value);
        }
    }
}
