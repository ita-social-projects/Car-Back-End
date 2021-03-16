using Car.Data;
using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    [TestFixture]
    public class UserPreferencesValidatorTest
    {
        private readonly UserPreferencesValidator validator;

        public UserPreferencesValidatorTest()
        {
            validator = new UserPreferencesValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(userPreferences => userPreferences.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(userPreferences => userPreferences.Id, value);
        }

        [Xunit.Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DoAllowSmoking_IsSpecified_NotGeneratesValidationError(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(userPreferences => userPreferences.DoAllowSmoking, value);
        }

        [Xunit.Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void DoAllowEating_IsSpecified_NotGeneratesValidationError(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(userPreferences => userPreferences.DoAllowEating, value);
        }

        [Fact]
        public void Comments_IsNotValid_GeneratesValidationError()
        {
            string longCommnt = new string('*', Constants.COMMENTS_MAX_LENGTH + 1);
            validator.ShouldHaveValidationErrorFor(userPreferences => userPreferences.Comments, longCommnt);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData("comment")]
        [InlineData(null)]
        public void Comments_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(userPreferences => userPreferences.Comments, value);
        }
    }
}
