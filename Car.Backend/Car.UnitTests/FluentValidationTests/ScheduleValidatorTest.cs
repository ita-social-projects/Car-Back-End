using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    [TestFixture]
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
        public void Should_have_error_when_Id_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(scheduleValidator => scheduleValidator.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_Id_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(scheduleValidator => scheduleValidator.Id, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_have_error_when_Name_is_not_valid(string value)
        {
            validator.ShouldHaveValidationErrorFor(scheduleValidator => scheduleValidator.Name, value);
        }

        [Xunit.Theory]
        [InlineData("Name")]
        public void Should_not_have_error_when_Name_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(scheduleValidator => scheduleValidator.Name, value);
        }
    }
}
