﻿using Car.Data.Constants;
using Car.Data.Entities;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Notification
{
    public class CreateNotificationModelValidatorTest
    {
        private readonly CreateNotificationDtoValidator validator;

        public CreateNotificationModelValidatorTest()
        {
            validator = new CreateNotificationDtoValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ReceiverId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(notificationModel => notificationModel.ReceiverId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void ReceiverId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(notificationModel => notificationModel.ReceiverId, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SenderId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(notificationModel => notificationModel.SenderId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void SenderId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(notificationModel => notificationModel.SenderId, value);
        }

        [Xunit.Theory]
        [InlineData(-5)]
        [InlineData(-1)]
        public void JourneyId_IsNotValid_GeneratesValidationError(int? value)
        {
            validator.ShouldHaveValidationErrorFor(notificationModel => notificationModel.JourneyId, value);
        }

        [Xunit.Theory]
        [InlineData(null)]
        [InlineData(10)]
        public void JourneyId_IsValid_NotGeneratesValidationError(int? value)
        {
            validator.ShouldNotHaveValidationErrorFor(notificationModel => notificationModel.JourneyId, value);
        }

        [Xunit.Theory]
        [InlineData(NotificationType.AcceptedInvitation)]
        public void Type_IsSpecified_NotGeneratesValidationError(NotificationType value)
        {
            validator.ShouldNotHaveValidationErrorFor(notificationModel => notificationModel.Type, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData(null)]
        public void JsonData_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(notificationModel => notificationModel.JsonData, value);
        }

        [Xunit.Theory]
        [InlineData("x")]
        public void JsonData_IsLessThanItRequires_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(notificationModel => notificationModel.JsonData, value);
        }

        [Xunit.Theory]
        [InlineData("asd")]
        public void JsonData_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(notificationModel => notificationModel.JsonData, value);
        }
    }
}
