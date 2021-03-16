using System;
using Car.Data;
using Car.Data.Entities;
using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    [TestFixture]
    public class NotificationValidatorTest
    {
        private readonly NotificationValidator validator;

        public NotificationValidatorTest()
        {
            validator = new NotificationValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(location => location.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(location => location.Id, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ReceiverId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(location => location.ReceiverId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void ReceiverId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(location => location.ReceiverId, value);
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SenderId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(location => location.SenderId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void SenderId_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(location => location.SenderId, value);
        }

        [Xunit.Theory]
        [InlineData(NotificationType.AcceptedInvitation)]
        public void Type_IsSpecified_NotGeneratesValidationError(NotificationType value)
        {
            validator.ShouldNotHaveValidationErrorFor(stop => stop.Type, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData(null)]
        public void JsonData_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(message => message.JsonData, value);
        }

        [Fact]
        public void JsonData_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.JSON_MAX_LENGTH + 1);
            validator.ShouldHaveValidationErrorFor(message => message.JsonData, longText);
        }

        [Xunit.Theory]
        [InlineData("asd")]
        public void JsonData_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(message => message.JsonData, value);
        }

        [Xunit.Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsRead__IsSpecified_NotGeneratesValidationError(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.IsRead, value);
        }

        [Xunit.Theory]
        [InlineData("2020-01-01")]
        public void CreatedAt_IsNotValid_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(journey => journey.CreatedAt, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData("2090-06-27")]
        public void CreatedAt__IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(journey => journey.CreatedAt, DateTime.Parse(value));
        }
    }
}
