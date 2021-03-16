using Car.Data;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    [TestFixture]
    public class UpdateUserModelValidatorTest
    {
        private readonly UpdateUserModelValidator validator;

        public UpdateUserModelValidatorTest()
        {
            validator = new UpdateUserModelValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(userModel => userModel.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(userModel => userModel.Id, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Name_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(userModel => userModel.Name, value);
        }

        [Fact]
        public void Name_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.STRING_MAX_LENGTH + 1);
            validator.ShouldHaveValidationErrorFor(userModel => userModel.Name, longText);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Name_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(userModel => userModel.Name, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Surname_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(userModel => userModel.Surname, value);
        }

        [Fact]
        public void Surname_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.STRING_MAX_LENGTH + 1);
            validator.ShouldHaveValidationErrorFor(userModel => userModel.Surname, longText);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Surname_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(userModel => userModel.Surname, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Position_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(userModel => userModel.Position, value);
        }

        [Fact]
        public void Position_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.POSITION_MAX_LENGTH + 1);
            validator.ShouldHaveValidationErrorFor(userModel => userModel.Position, longText);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Position_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(userModel => userModel.Position, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Location_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(userModel => userModel.Location, value);
        }

        [Fact]
        public void Location_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.LOCATION_MAX_LENGTH + 1);
            validator.ShouldHaveValidationErrorFor(userModel => userModel.Location, longText);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void Location_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(userModel => userModel.Location, value);
        }
    }
}
