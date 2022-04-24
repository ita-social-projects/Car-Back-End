using Car.Data.Enums;
using Car.Data.FluentValidation;
using Car.UnitTests.Base;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.User
{
    public class UserStopValidatorTest
    {
        private readonly UserStopValidator validator;

        public UserStopValidatorTest()
        {
            this.validator = new UserStopValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UserId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(userStop => userStop.UserId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void UserId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(userStop => userStop.UserId, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void StopId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(userStop => userStop.StopId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void StopId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(userStop => userStop.StopId, value);
        }
    }
}