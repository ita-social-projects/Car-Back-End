using Car.Data.Constants;
using Car.Data.Enums;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Car
{
    public class UpdateCarDtoValidatorTest
    {
        private readonly UpdateCarDtoValidator validator;

        public UpdateCarDtoValidatorTest()
        {
            validator = new UpdateCarDtoValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(carModel => carModel.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(carModel => carModel.Id, value);
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
        [InlineData(null)]
        public void PlateNumber_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(carModel => carModel.PlateNumber, value);
        }
    }
}
