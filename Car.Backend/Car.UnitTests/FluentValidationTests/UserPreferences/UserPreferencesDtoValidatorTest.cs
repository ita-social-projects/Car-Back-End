﻿using Car.Data.Constants;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using Xunit;
using TheoryAttribute = Xunit.TheoryAttribute;

namespace Car.UnitTests.FluentValidationTests.UserPreferences
{
    public class UserPreferencesDtoValidatorTest
    {
        private UserPreferencesDtoValidator validator;

        public UserPreferencesDtoValidatorTest()
        {
            validator = new UserPreferencesDtoValidator();
        }

        [Fact]
        public void PreferencesComment_IsNotValid_GeneratesValidationError()
        {
            var longComment = new string('-', Constants.CommentsMaxLength + 1);
            validator.ShouldHaveValidationErrorFor(preferences => preferences.Comments, longComment);
        }

        [Theory]
        [InlineData("")]
        [InlineData("I am good person")]
        public void PreferencesComment_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(preferences => preferences.Comments, value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void PreferencesDoAllowEating_IsSpecified_NotGeneratesValidationError(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(preferences => preferences.DoAllowEating, value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void PreferencesDoAllowSmoking_IsSpecified_NotGeneratesValidationError(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(preferences => preferences.DoAllowSmoking, value);
        }
    }
}
