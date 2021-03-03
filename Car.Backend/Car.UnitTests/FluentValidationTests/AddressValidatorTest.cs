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
        public void Should_have_error_when_Id_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(address => address.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_Id_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.Id, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_have_error_when_Street_is_null(string value)
        {
            validator.ShouldHaveValidationErrorFor(address => address.Street, value);
        }

        [Xunit.Theory]
        [InlineData("StreetName")]
        public void Should_not_have_error_when_Street_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.Street, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_have_error_when_City_is_null(string value)
        {
            validator.ShouldHaveValidationErrorFor(address => address.City, value);
        }

        [Xunit.Theory]
        [InlineData("CityName")]
        public void Should_not_have_error_when_City_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.City, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Should_not_have_error_when_Latitude_is_specified(double value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.Latitude, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Should_not_have_error_when_Longitude_is_specified(double value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.Longitude, value);
        }
    }
}
