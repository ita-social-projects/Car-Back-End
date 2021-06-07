using AutoFixture.Xunit2;
using Car.Data.FluentValidation;
using Car.Domain.Dto;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;
using RangeAttr = System.ComponentModel.DataAnnotations.RangeAttribute;

namespace Car.UnitTests.FluentValidationTests.JourneyPoint
{
    [TestFixture]
    public class JourneyPointDtoValidatorTest
    {
        private readonly JourneyPointDtoValidator validator;

        public JourneyPointDtoValidatorTest()
        {
            validator = new JourneyPointDtoValidator();
        }

        [Xunit.Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        public void Index_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyPointDto => journeyPointDto.Index, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Index_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyPointDto => journeyPointDto.Index, value);
        }

        [Xunit.Theory]
        [InlineData(-100)]
        [InlineData(100)]
        public void Latitude_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyPointDto => journeyPointDto.Latitude, value);
        }

        [Xunit.Theory]
        [AutoData]
        public void Latitude_IsSpecified_NotGeneratesValidationError([RangeAttr(-90, 90)] int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyPointDto => journeyPointDto.Latitude, value);
        }

        [Xunit.Theory]
        [InlineData(-200)]
        [InlineData(200)]
        public void Longitude_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyPointDto => journeyPointDto.Longitude, value);
        }

        [Xunit.Theory]
        [AutoData]
        public void Longitude_IsSpecified_NotGeneratesValidationError([RangeAttr(-180, 180)] int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyPointDto => journeyPointDto.Longitude, value);
        }
    }
}