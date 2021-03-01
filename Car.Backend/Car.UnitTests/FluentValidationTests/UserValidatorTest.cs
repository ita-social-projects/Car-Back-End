using System;
using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    [TestFixture]
    public class UserValidatorTest
    {
        private readonly UserValidator validator;

        public UserValidatorTest()
        {
            validator = new UserValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_have_error_when_Id_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(user => user.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_Id_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(user => user.Id, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_have_error_when_Name_is_null(string value)
        {
            validator.ShouldHaveValidationErrorFor(user => user.Name, value);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Should_not_have_error_when_Name_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(user => user.Name, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_have_error_when_Surname_is_null(string value)
        {
            validator.ShouldHaveValidationErrorFor(user => user.Surname, value);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Should_not_have_error_when_Surname_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(user => user.Surname, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_have_error_when_Position_is_null(string value)
        {
            validator.ShouldHaveValidationErrorFor(user => user.Surname, value);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Should_not_have_error_when_Position_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(user => user.Surname, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_have_error_when_Location_is_null(string value)
        {
            validator.ShouldHaveValidationErrorFor(user => user.Surname, value);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Should_not_have_error_when_Location_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(user => user.Surname, value);
        }

        [Xunit.Theory]
        [InlineData("2090-06-27")]
        public void Should_have_error_when_HireDate_is_not_valid(string value)
        {
            validator.ShouldHaveValidationErrorFor(user => user.HireDate, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData("2013-06-27")]
        public void Should_not_have_error_when_HireDate_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(user => user.HireDate, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_have_error_when_Email_is_null(string value)
        {
            validator.ShouldHaveValidationErrorFor(user => user.Email, value);
        }

        [Xunit.Theory]
        [InlineData("abc@gmail.com")]
        public void Should_not_have_error_when_Email_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(user => user.Email, value);
        }
    }
}
