using System;
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
        public void Should_have_error_when_Id_is_not_valid(int value)
        {
            validator.ShouldHaveValidationErrorFor(notification => notification.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Should_not_have_error_when_Id_is_specified(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(notification => notification.Id, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Should_have_error_when_Description_is_null(string value)
        {
            validator.ShouldHaveValidationErrorFor(notification => notification.Description, value);
        }

        [Fact]
        public void Should_have_error_when_Description_is_longer_than_401()
        {
            string longDescription = new string('*', 401);
            validator.ShouldHaveValidationErrorFor(notification => notification.Description, longDescription);
        }

        [Xunit.Theory]
        [InlineData("comment")]
        public void Should_not_have_error_when_Description_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(notification => notification.Description, value);
        }

        [Xunit.Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Should_not_have_error_when_IsRead_is_specified(bool value)
        {
            validator.ShouldNotHaveValidationErrorFor(notification => notification.IsRead, value);
        }

        [Xunit.Theory]
        [InlineData("2020-01-01")]
        public void Should_have_error_when_CreateAt_is_not_valid(string value)
        {
            validator.ShouldHaveValidationErrorFor(notification => notification.CreatedAt, DateTime.Parse(value));
        }

        [Xunit.Theory]
        [InlineData("2090-06-27")]
        public void Should_not_have_error_when_CreateAt_is_specified(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(notification => notification.CreatedAt, DateTime.Parse(value));
        }
    }
}
