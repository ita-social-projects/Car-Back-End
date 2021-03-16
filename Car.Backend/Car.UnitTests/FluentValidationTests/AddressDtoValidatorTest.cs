using Car.Data;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    [TestFixture]
    public class AddressDtoValidatorTest
    {
        private readonly AddressDtoValidator validator;

        public AddressDtoValidatorTest()
        {
            validator = new AddressDtoValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(addressDto => addressDto.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(addressDto => addressDto.Id, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Street_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(addressDto => addressDto.Street, value);
        }

        [Fact]
        public void Street_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.STRING_MAX_LENGTH + 1);
            validator.ShouldHaveValidationErrorFor(addressDto => addressDto.Street, longText);
        }

        [Xunit.Theory]
        [InlineData("StreetName")]
        public void Street_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(addressDto => addressDto.Street, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData(null)]
        public void City_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(addressDto => addressDto.City, value);
        }

        [Fact]
        public void City_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.STRING_MAX_LENGTH + 1);
            validator.ShouldHaveValidationErrorFor(addressDto => addressDto.City, longText);
        }

        [Xunit.Theory]
        [InlineData("CityName")]
        public void City_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(addressDto => addressDto.City, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Latitude_IsSpecified_NotGeneratesValidationError(double value)
        {
            validator.ShouldNotHaveValidationErrorFor(addressDto => addressDto.Latitude, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Longitude_IsSpecified_NotGeneratesValidationError(double value)
        {
            validator.ShouldNotHaveValidationErrorFor(addressDto => addressDto.Longitude, value);
        }
    }
}
