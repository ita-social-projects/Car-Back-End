using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.JourneyPoint
{
    [TestFixture]
    public class JourneyPointDtoValidatorTest
    {
        private readonly JourneyPointValidator validator;

        public JourneyPointDtoValidatorTest()
        {
            validator = new JourneyPointValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyPointDto => journeyPointDto.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyPointDto => journeyPointDto.Id, value);
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
        [InlineData(0)]
        [InlineData(90)]
        public void Latitude_IsSpecified_NotGeneratesValidationError(int value)
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
        [InlineData(0)]
        [InlineData(180)]
        public void Longitude_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyPointDto => journeyPointDto.Longitude, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void JourneyId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyPointDto => journeyPointDto.JourneyId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void JourneyId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyPointDto => journeyPointDto.JourneyId, value);
        }
    }
}