using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    [TestFixture]
    public class AddressValidatorTest
    {
        private readonly AddressValidator validator;

        public AddressValidatorTest()
        {
            validator = new AddressValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(address => address.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.Id, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Street_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(address => address.Street, value);
        }

        [Xunit.Theory]
        [InlineData("StreetName")]
        public void Street_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.Street, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData(null)]
        public void City_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(address => address.City, value);
        }

        [Xunit.Theory]
        [InlineData("CityName")]
        public void City_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.City, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Latitude_IsNotValid_GeneratesValidationError(double value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.Latitude, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Longitude_IsSpecified_NotGeneratesValidationError(double value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.Longitude, value);
        }
    }
}
