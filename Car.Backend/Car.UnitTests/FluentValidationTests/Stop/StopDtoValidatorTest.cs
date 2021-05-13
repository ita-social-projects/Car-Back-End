using Car.Data.Enums;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Stop
{
    [TestFixture]
    public class StopDtoValidatorTest
    {
        private StopDtoValidator validator;

        public StopDtoValidatorTest()
        {
            validator = new StopDtoValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(stopDto => stopDto.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(stopDto => stopDto.Id, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UserId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(stopDto => stopDto.UserId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void UserId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(stopDto => stopDto.UserId, value);
        }

        [Xunit.Theory]
        [InlineData(StopType.Start)]
        public void Type_IsSpecified_NotGeneratesValidationError(StopType value)
        {
            validator.ShouldNotHaveValidationErrorFor(stopDto => stopDto.Type, value);
        }
    }
}
