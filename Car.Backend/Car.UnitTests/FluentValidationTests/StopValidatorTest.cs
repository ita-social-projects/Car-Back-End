using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    [TestFixture]
    public class StopValidatorTest
    {
        private StopValidator validator;

        public StopValidatorTest()
        {
            validator = new StopValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_have_error_when_Id_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(stop => stop.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_Id_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(stop => stop.Id, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_have_error_when_JourneyId_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(stop => stop.JourneyId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_JourneyId_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(stop => stop.JourneyId, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_have_error_when_AddressId_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(stop => stop.AddressId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_AddressId_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(stop => stop.AddressId, value);
        }
    }
}
