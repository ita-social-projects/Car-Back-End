using AutoFixture.Xunit2;
using Car.Data.Constants;
using Car.Data.FluentValidation;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;
using RangeAttr = System.ComponentModel.DataAnnotations.RangeAttribute;

namespace Car.UnitTests.FluentValidationTests.Address
{
    [TestFixture]
    public class UpdateAddressToLocationModelValidatorTest
    {
        private readonly UpdateAddressToLocationModelValidator validator;

        public UpdateAddressToLocationModelValidatorTest()
        {
            validator = new UpdateAddressToLocationModelValidator();
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
        [AutoData]
        public void Latitude_IsSpecified_NotGeneratesValidationError([RangeAttr(Constants.MinLatitude, Constants.MaxLatitude)]double value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.Latitude, value);
        }

        [Xunit.Theory]
        [InlineData(Constants.MaxLatitude + 1)]
        [InlineData(Constants.MinLatitude - 1)]
        public void Latitude_IsNotCorrect_GeneratesValidationError(double value)
        {
            validator.ShouldHaveValidationErrorFor(address => address.Latitude, value);
        }

        [Xunit.Theory]
        [AutoData]
        public void Longitude_IsSpecified_NotGeneratesValidationError(
            [RangeAttr(Constants.MinLongitude, Constants.MaxLongitude)] double value)
        {
            validator.ShouldNotHaveValidationErrorFor(address => address.Longitude, value);
        }

        [Xunit.Theory]
        [InlineData(Constants.MaxLongitude + 1)]
        [InlineData(Constants.MinLongitude - 1)]
        public void Longtitide_IsNotCorrect_GeneratesValidationError(double value)
        {
            validator.ShouldHaveValidationErrorFor(address => address.Longitude, value);
        }
    }
}
