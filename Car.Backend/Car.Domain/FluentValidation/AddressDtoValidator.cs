﻿using Car.Data;
using FluentValidation;

namespace Car.Domain.FluentValidation
{
    public class AddressDtoValidator : AbstractValidator<Dto.AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(address => address.Id).GreaterThan(Constants.ID_LENGTH);
            RuleFor(address => address.Name).NotNull().NotEmpty();
            RuleFor(address => address.Latitude).NotNull();
            RuleFor(address => address.Longitude).NotNull();
        }
    }
}
