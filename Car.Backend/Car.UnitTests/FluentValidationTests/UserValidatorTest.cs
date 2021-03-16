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
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(user => user.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(user => user.Id, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Name_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(user => user.Name, value);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Name_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(user => user.Name, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Surname_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(user => user.Surname, value);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Surname_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(user => user.Surname, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Position_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(user => user.Surname, value);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Position_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(user => user.Surname, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Location_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(user => user.Surname, value);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Location_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(user => user.Surname, value);
        }

        [Xunit.Theory]
        [InlineData("2090-06-27")]
        public void HireDate_IsNotValid_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(user => user.HireDate, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData("2013-06-27")]
        public void HireDate_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(user => user.HireDate, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Email_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(user => user.Email, value);
        }

        [Xunit.Theory]
        [InlineData("abc@gmail.com")]
        public void Email_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(user => user.Email, value);
        }
    }
}
