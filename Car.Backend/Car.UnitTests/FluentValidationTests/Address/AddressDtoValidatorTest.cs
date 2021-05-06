using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

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
