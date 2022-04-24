using Car.Data.Enums;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Stop
{
    public class StopDtoValidatorTest
    {
        private readonly StopDtoValidator validator;

        public StopDtoValidatorTest()
        {
            validator = new StopDtoValidator();
        }

        [Xunit.Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        public void Index_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(stopDto => stopDto.Index, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Index_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(stopDto => stopDto.Index, value);
        }
    }
}
