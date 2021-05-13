using Car.Data.Constants;
using Car.Data.Enums;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Car
{
    [TestFixture]
    public class CreateCarModelValidatorTest
    {
        private CreateCarModelValidator validator;

        public CreateCarModelValidatorTest()
        {
            validator = new CreateCarModelValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void OwnerId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(carModel => carModel.OwnerId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void OwnerId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(carModel => carModel.OwnerId, value);
        }

        [Xunit.Theory]
        [InlineData(Color.Black)]
        public void Color_IsSpecified_NotGeneratesValidationError(Color value)
        {
            validator.ShouldNotHaveValidationErrorFor(carModel => carModel.Color, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData("123")]
        [InlineData(null)]
        public void PlateNumber_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(carModel => carModel.PlateNumber, value);
        }

        [Fact]
        public void PlateNumber__IsNotValid_GeneratesValidationError()
        {
            string longCommnt = new string('*', Constants.PlateNumberMaxLength + 1);
            validator.ShouldHaveValidationErrorFor(carModel => carModel.PlateNumber, longCommnt);
        }

        [Xunit.Theory]
        [InlineData("12345")]
        public void PlateNumber_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(carModel => carModel.PlateNumber, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ModelId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(carModel => carModel.ModelId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void ModelId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(carModel => carModel.ModelId, value);
        }
    }
}
