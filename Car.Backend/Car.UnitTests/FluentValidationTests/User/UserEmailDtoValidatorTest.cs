using System;
using Car.Data.Constants;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.User
{
    public class UserEmailDtoValidatorTest
    {
        private readonly UserEmailDtoValidator validator;

        public UserEmailDtoValidatorTest()
        {
            validator = new UserEmailDtoValidator();
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Email_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(userDto => userDto.Email, value);
        }

        [Fact]
        public void Email_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.EmailMaxLength + 1);
            validator.ShouldHaveValidationErrorFor(userDto => userDto.Email, longText);
        }

        [Xunit.Theory]
        [InlineData("abc@gmail.com")]
        public void Email_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(userDto => userDto.Email, value);
        }
    }
}
