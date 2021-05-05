using System;
using Car.Data;
using Car.Data.Constants;
using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    [TestFixture]
    public class MessageValidatorTest
    {
        private MessageValidator validator;

        public MessageValidatorTest()
        {
            validator = new MessageValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(message => message.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(message => message.Id, value);
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
        [InlineData("2020-01-01")]
        public void CreateAt_IsNotValid_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(message => message.CreatedAt, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData("2090-06-27")]
        public void CreateAt_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(message => message.CreatedAt, DateTime.Parse(value));
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
    }
}
