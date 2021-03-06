﻿using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class BrandValidator : AbstractValidator<Entities.Brand>
    {
        public BrandValidator()
        {
            RuleFor(brand => brand.Id).GreaterThan(Constants.Constants.IdLength);
            RuleFor(brand => brand.Name).NotNull().NotEmpty().MaximumLength(Constants.Constants.StringMaxLength);
        }
    }
}
