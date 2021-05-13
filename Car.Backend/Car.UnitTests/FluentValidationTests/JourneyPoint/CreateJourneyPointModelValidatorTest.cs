using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.JourneyPoint
{
    [TestFixture]
    public class CreateJourneyPointModelValidatorTest
    {
        private readonly CreateJourneyPointModelValidator validator;

        public CreateJourneyPointModelValidatorTest()
        {
            validator = new CreateJourneyPointModelValidator();
        }

        [Xunit.Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        public void Index_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyPoint => journeyPoint.Index, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Index_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyPoint => journeyPoint.Index, value);
        }

        [Xunit.Theory]
        [InlineData(-100)]
        [InlineData(100)]
        public void Latitude_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyPoint => journeyPoint.Latitude, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(90)]
        public void Latitude_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyPoint => journeyPoint.Latitude, value);
        }

        [Xunit.Theory]
        [InlineData(-200)]
        [InlineData(200)]
        public void Longitude_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(journeyPoint => journeyPoint.Longitude, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(180)]
        public void Longitude_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(journeyPoint => journeyPoint.Longitude, value);
        }
    }
}