using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.JourneyPoint
{
    [TestFixture]
    public class CreateJourneyPointModelTest
    {
        private readonly CreateJourneyPointModelValidator validator;

        public CreateJourneyPointModelTest()
        {
            validator = new CreateJourneyPointModelValidator();
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
    }
}