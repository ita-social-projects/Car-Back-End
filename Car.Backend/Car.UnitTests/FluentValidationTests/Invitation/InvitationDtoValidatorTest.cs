using System;
using Car.Data.Enums;
using Car.Domain.FluentValidation;
using FluentValidation.TestHelper;
using Xunit;

namespace Car.UnitTests.FluentValidationTests.Request
{
    public class InvitationDtoValidatorTest
    {
        private readonly InvitationDtoValidator validator;

        public InvitationDtoValidatorTest()
        {
            validator = new InvitationDtoValidator();
        }

        [Xunit.Theory]
        [InlineData(InvitationType.Sent)]
        public void InvitationType_IsValid_NotGeneratesValidationError(InvitationType value)
        {
            validator.ShouldNotHaveValidationErrorFor(invitation => invitation.Type, value);
        }

        [Xunit.Theory]
        [InlineData(-3)]
        [InlineData(0)]
        public void InvitedUserId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(invitation => invitation.InvitedUserId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(4)]
        public void InvitedUserId_IsValid_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(invitation => invitation.InvitedUserId, value);
        }

        [Xunit.Theory]
        [InlineData(-3)]
        [InlineData(0)]
        public void JourneyId_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(invitation => invitation.JourneyId, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(4)]
        public void JourneyId_IsValid_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(invitation => invitation.JourneyId, value);
        }
    }
}
