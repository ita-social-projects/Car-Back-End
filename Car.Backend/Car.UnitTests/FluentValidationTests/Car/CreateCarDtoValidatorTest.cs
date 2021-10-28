using Car.Data.Constants;
using Car.Data.Enums;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Car
{
    public class CreateCarDtoValidatorTest
    {
        private readonly CreateCarDtoValidator validator;

        public CreateCarDtoValidatorTest()
        {
            validator = new CreateCarDtoValidator();
        }

        [Xunit.Theory]
        [InlineData(Color.Black)]
        public void Color_IsSpecified_NotGeneratesValidationError(Color value)
        {
            validator.ShouldNotHaveValidationErrorFor(carModel => carModel.Color, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData("12345678910")]
        public void PlateNumber_IsNotValid_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(carModel => carModel.PlateNumber, value);
        }

        [Fact]
        public void PlateNumber_IsTooLong_GeneratesValidationError()
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
        [InlineData("")]
        [InlineData(null)]
        public void Model_IsNotValid_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(carModel => carModel.Model, value);
        }

        [Xunit.Theory]
        [InlineData("Qashkai")]
        public void Model_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(carModel => carModel.Model, value);
        }
    }
}
