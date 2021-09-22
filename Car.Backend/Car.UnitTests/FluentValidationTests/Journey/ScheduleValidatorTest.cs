using Car.Data.Constants;
using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Journey
{
    public class ScheduleValidatorTest
    {
        private readonly ScheduleValidator validator;

        public ScheduleValidatorTest()
        {
            validator = new ScheduleValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(schedule => schedule.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(schedule => schedule.Id, value);
        }
    }
}
