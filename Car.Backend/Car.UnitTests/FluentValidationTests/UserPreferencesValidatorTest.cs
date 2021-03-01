using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    [TestFixture]
    public class UserPreferencesValidatorTest
    {
        private UserPreferencesValidator validator;

        public UserPreferencesValidatorTest()
        {
            validator = new UserPreferencesValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_have_error_when_Id_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(userPreferences => userPreferences.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_Id_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(userPreferences => userPreferences.Id, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_have_error_when_UserId_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(userPreferences => userPreferences.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_UserId_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(userPreferences => userPreferences.Id, value);
        }

        [Xunit.Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Should_not_have_error_when_DoAllowSmoking_is_specified(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(userPreferences => userPreferences.DoAllowSmoking, value);
        }

        [Xunit.Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Should_not_have_error_when_DoAllowEating_is_specified(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(userPreferences => userPreferences.DoAllowEating, value);
        }

        [Fact]
        public void Should_have_error_when_Comments_is_longer_than_101()
        {
            string longCommnt = new string('*', 101);
            validator.ShouldHaveValidationErrorFor(userPreferences => userPreferences.Comments, longCommnt);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData("comment")]
        [InlineData(null)]
        public void Should_not_have_error_when_Comments_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(userPreferences => userPreferences.Comments, value);
        }
    }
}
