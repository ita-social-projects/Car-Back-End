using System;
using AutoFixture.Xunit2;
using Car.Data.Constants;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;
using RangeAttr = System.ComponentModel.DataAnnotations.RangeAttribute;

namespace Car.UnitTests.FluentValidationTests.Journey
{
    [TestFixture]
    public class JourneyDtoValidatorTest
    {
        private readonly JourneyDtoValidator validator;

        public JourneyDtoValidatorTest()
        {
            validator = new JourneyDtoValidator();
        }

        [Xunit.Theory]
        [InlineData("2020-01-01")]
        public void DepartureTime_IsNotValid_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(journeyDto => journeyDto.DepartureTime, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData("2090-06-27")]
        public void DepartureTime_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyDto => journeyDto.DepartureTime, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(5)]
        public void CountOfSeats_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyDto => journeyDto.CountOfSeats, value);
        }

        [Xunit.Theory]
        [AutoData]
        public void CountOfSeats_IsSpecified_NotGeneratesValidationError([RangeAttr(1, 4)] int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyDto => journeyDto.CountOfSeats, value);
        }

        [Fact]
        public void Duration_IsNotValid_GeneratesValidationError()
        {
            var incorrectDuration = TimeSpan.MinValue;

            validator.ShouldHaveValidationErrorFor(journeyDto => journeyDto.Duration, incorrectDuration);
        }

        [Fact]
        public void DurationInMinutes_IsSpecified_NotGeneratesValidationError()
        {
            var correctDuration = TimeSpan.MaxValue;

            validator.ShouldNotHaveValidationErrorFor(journeyDto => journeyDto.Duration, correctDuration);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void RouteDistance_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyDto => journeyDto.RouteDistance, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void RouteDistance_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyDto => journeyDto.RouteDistance, value);
        }

        [Fact]
        public void Comments_IsNotValid_GeneratesValidationError()
        {
            var longComment = new string('*', Constants.CommentsMaxLength + 1);
            validator.ShouldHaveValidationErrorFor(journeyDto => journeyDto.Comments, longComment);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData("comment")]
        [InlineData(null)]
        public void Comments_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyDto => journeyDto.Comments, value);
        }

        [Xunit.Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsFree_IsSpecified_NotGeneratesValidationError(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyDto => journeyDto.IsFree, value);
        }

        [Xunit.Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsOnOwnCar_IsSpecified_NotGeneratesValidationError(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyDto => journeyDto.IsOnOwnCar, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void OrganizerId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyDto => journeyDto.OrganizerId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void OrganizerId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyDto => journeyDto.OrganizerId, value);
        }
    }
}