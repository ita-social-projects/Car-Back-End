﻿using Car.Data.FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using Xunit;

namespace Car.UnitTests.FluentValidationTests
{
    [TestFixture]
    public class ScheduleValidatorTest
    {
        private readonly ScheduleValidator validator;

        public ScheduleValidatorTest()
        {
            validator = new ScheduleValidator();
        }

        [Xunit.Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Id_IsNotValid_GeneratesValidationError(int value)
        {
            validator.ShouldHaveValidationErrorFor(scheduleValidator => scheduleValidator.Id, value);
        }

        [Xunit.Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void Id_IsSpecified_NotGeneratesValidationError(int value)
        {
            validator.ShouldNotHaveValidationErrorFor(scheduleValidator => scheduleValidator.Id, value);
        }

        [Xunit.Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Name_IsNull_GeneratesValidationError(string value)
        {
            validator.ShouldHaveValidationErrorFor(scheduleValidator => scheduleValidator.Name, value);
        }

        [Xunit.Theory]
        [InlineData("Name")]
        public void Name_IsSpecified_NotGeneratesValidationError(string value)
        {
            validator.ShouldNotHaveValidationErrorFor(scheduleValidator => scheduleValidator.Name, value);
        }
    }
}
