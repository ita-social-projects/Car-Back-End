using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void Should_have_error_when_Id_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(message => message.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_Id_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(message => message.Id, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        public void Should_have_error_when_Text_is_null(string value)
        {
            validator.ShouldHaveValidationErrorFor(message => message.Text, value);
        }

        [Fact]
        public void Should_have_error_when_Comments_is_longer_than_401()
        {
            string longText = new string('*', 401);
            validator.ShouldHaveValidationErrorFor(message => message.Text, longText);
        }

        [Xunit.Theory]
        [InlineData("asd")]
        public void Should_not_have_error_when_Text_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(message => message.Text, value);
        }

        [Xunit.Theory]
        [InlineData("2020-01-01")]
        public void Should_have_error_when_CreateAt_is_not_valid(string value)
        {
            validator.ShouldHaveValidationErrorFor(message => message.CreatedAt, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData("2090-06-27")]
        public void Should_not_have_error_when_CreateAt_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(message => message.CreatedAt, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_have_error_when_SenderId_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(message => message.SenderId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_SenderId_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(message => message.SenderId, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_have_error_when_ReceiverId_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(message => message.ChatId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_ReceiverId_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(message => message.ChatId, value);
        }
    }
}
