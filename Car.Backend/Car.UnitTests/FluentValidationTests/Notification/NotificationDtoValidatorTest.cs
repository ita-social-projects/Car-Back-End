﻿using Car.Data.Constants;
using Car.Data.Entities;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Notification
{
    [TestFixture]
    public class NotificationDtoValidatorTest
    {
        private readonly NotificationDtoValidator validator;

        public NotificationDtoValidatorTest()
        {
            validator = new NotificationDtoValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ReceiverId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(notification => notification.ReceiverId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void ReceiverId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(notification => notification.ReceiverId, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SenderId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(notification => notification.SenderId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void SenderId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(notification => notification.SenderId, value);
        }

        [Xunit.Theory]
        [InlineData(NotificationType.AcceptedInvitation)]
        public void Type_IsSpecified_NotGeneratesValidationError(NotificationType value)
        {
            validator.ShouldNotHaveValidationErrorFor(notification => notification.Type, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData(null)]
        public void JsonData_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(notification => notification.JsonData, value);
        }

        [Fact]
        public void JsonData_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.JsonMaxLength + 1);
            validator.ShouldHaveValidationErrorFor(notification => notification.JsonData, longText);
        }

        [Xunit.Theory]
        [InlineData("asd")]
        public void JsonData_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(notification => notification.JsonData, value);
        }
    }
}