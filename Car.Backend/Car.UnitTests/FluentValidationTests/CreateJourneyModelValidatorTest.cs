using System;
using Car.Data;
using Car.Data.Constants;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    [TestFixture]
    public class CreateJourneyModelValidatorTest
    {
        private readonly CreateJourneyModelValidator validator;

        public CreateJourneyModelValidatorTest()
        {
            validator = new CreateJourneyModelValidator();
        }

        [Xunit.Theory]
        [InlineData("2020-01-01")]
        public void DepartureTime_IsNotValid_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(journeyModel => journeyModel.DepartureTime, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData("2090-06-27")]
        public void DepartureTime_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyModel => journeyModel.DepartureTime, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(5)]
        public void CountOfSeats_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyModel => journeyModel.CountOfSeats, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(4)]
        public void CountOfSeats_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyModel => journeyModel.CountOfSeats, value);
        }

        [Fact]
        public void Comments_IsNotValid_GeneratesValidationError()
        {
            var longComment = new string('*', Constants.CommentsMaxLength + 1);
            validator.ShouldHaveValidationErrorFor(journeyModel => journeyModel.Comments, longComment);
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
        public void IsFree_IsSpecified_NotGeneratesValidationError(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyModel => journeyModel.IsFree, value);
        }

        [Xunit.Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsOnOwnCar_IsSpecified_NotGeneratesValidationError(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyModel => journeyModel.IsOnOwnCar, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void OrganizerId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyModel => journeyModel.OrganizerId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void OrganizerId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyModel => journeyModel.OrganizerId, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CarId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyModel => journeyModel.CarId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void CarId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyModel => journeyModel.CarId, value);
        }
    }
}