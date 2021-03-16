using Car.Data;
using Car.Data.Entities;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    [TestFixture]
    public class CreateNotificationModelValidatorTest
    {
        private readonly CreateNotificationModelValidator validator;

        public CreateNotificationModelValidatorTest()
        {
            validator = new CreateNotificationModelValidator();
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

        [Fact]
        public void JsonData_IsNotValid_GeneratesValidationError()
        {
            string longText = new string('*', Constants.JSON_MAX_LENGTH + 1);
            validator.ShouldHaveValidationErrorFor(notificationModel => notificationModel.JsonData, longText);
        }

        [Xunit.Theory]
        [InlineData("asd")]
        public void JsonData_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(notificationModel => notificationModel.JsonData, value);
        }
    }
}
