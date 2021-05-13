using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Address
{
    [TestFixture]
    public class CreateAddressModelValidatorTest
    {
        private readonly CreateAddressModelValidator validator;

        public CreateAddressModelValidatorTest()
        {
            validator = new CreateAddressModelValidator();
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Name_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(address => address.Name, value);
        }

        [Xunit.Theory]
        [InlineData("Address Name")]
        public void Name_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.Name, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(90)]
        public void Latitude_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.Latitude, value);
        }

        [Xunit.Theory]
        [InlineData(-200)]
        [InlineData(200)]
        public void Longitude_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(address => address.Longitude, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(180)]
        public void Longitude_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.Longitude, value);
        }
    }
}