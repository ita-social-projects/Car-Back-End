using System;
using Car.Data.Constants;
using Car.Data.FluentValidation;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Message
{
    public class MessageDtoValidatorTest
    {
        private MessageDtoValidator validator;

        public MessageDtoValidatorTest()
        {
            validator = new MessageDtoValidator();
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Text_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(message => message.Text, value);
        }

        [Fact]
        public void Text_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.TextMaxLength + 1);
            validator.ShouldHaveValidationErrorFor(message => message.Text, longText);
        }

        [Xunit.Theory]
        [InlineData("asd")]
        public void Text_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(message => message.Text, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SenderId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(message => message.SenderId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void SenderId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(message => message.SenderId, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ChatId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(message => message.ChatId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void ChatId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(message => message.ChatId, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SenderName_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(message => message.SenderName, value);
        }

        [Fact]
        public void SenderName_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.StringMaxLength + 1);
            validator.ShouldHaveValidationErrorFor(message => message.SenderName, longText);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void SenderName_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(message => message.SenderName, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SenderSurname_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(message => message.SenderSurname, value);
        }

        [Fact]
        public void SenderSurname_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.StringMaxLength + 1);
            validator.ShouldHaveValidationErrorFor(message => message.SenderSurname, longText);
        }

        [Xunit.Theory]
        [InlineData("abc")]
        public void SenderSurname_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(message => message.SenderSurname, value);
        }
    }
}
