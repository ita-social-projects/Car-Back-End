using System;
using Car.Data.Constants;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.User
{
    public class UserDtoValidatorTest
    {
        private readonly UserDtoValidator validator;

        public UserDtoValidatorTest()
        {
            validator = new UserDtoValidator();
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(userDto => userDto.Id, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Name_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(userDto => userDto.Name, value);
        }

        [Fact]
        public void Name_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.StringMaxLength + 1);
            validator.ShouldHaveValidationErrorFor(userDto => userDto.Name, longText);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Name_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(userDto => userDto.Name, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Surname_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(userDto => userDto.Surname, value);
        }

        [Fact]
        public void Surname_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.StringMaxLength + 1);
            validator.ShouldHaveValidationErrorFor(userDto => userDto.Surname, longText);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Surname_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(userDto => userDto.Surname, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Position_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(userDto => userDto.Position, value);
        }

        [Fact]
        public void Position_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.PositionMaxLength + 1);
            validator.ShouldHaveValidationErrorFor(userDto => userDto.Position, longText);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Position_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(userDto => userDto.Position, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Location_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(userDto => userDto.Location, value);
        }

        [Fact]
        public void Location_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.LocationMaxLength + 1);
            validator.ShouldHaveValidationErrorFor(userDto => userDto.Location, longText);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Location_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(userDto => userDto.Location, value);
        }

        [Xunit.Theory]
        [InlineData("2090-06-27")]
        public void HireDate_IsNotValid_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(userDto => userDto.HireDate, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData("2013-06-27")]
        public void HireDate_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(userDto => userDto.HireDate, DateTime.Parse(value));
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
