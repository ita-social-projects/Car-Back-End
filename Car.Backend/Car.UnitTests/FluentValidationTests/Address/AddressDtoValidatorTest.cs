using AutoFixture.Xunit2;
using Car.Data.Constants;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;
using RangeAttr = System.ComponentModel.DataAnnotations.RangeAttribute;

namespace Car.UnitTests.FluentValidationTests.Address
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
        [InlineData("")]
        [InlineData(null)]
        public void Name_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(addressDto => addressDto.Name, value);
        }

        [Xunit.Theory]
        [InlineData("Address Name")]
        public void Name_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(addressDto => addressDto.Name, value);
        }

        [Xunit.Theory]
        [AutoData]
        public void Latitude_IsSpecified_NotGeneratesValidationError([RangeAttr(Constants.MinLatitude, Constants.MaxLatitude)] int value)
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
        [AutoData]
        public void Longitude_IsSpecified_NotGeneratesValidationError([RangeAttr(Constants.MinLongitude, Constants.MaxLongitude)] int value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.Longitude, value);
        }
    }
}
