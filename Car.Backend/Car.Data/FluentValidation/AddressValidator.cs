using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class AddressValidator : AbstractValidator<Entities.Address>
    {
        public AddressValidator()
        {
            RuleFor(addres => addres.Id).GreaterThan(0);
            RuleFor(address => address.Street).NotNull().Length(2, 50);
            RuleFor(address => address.City).NotNull().Length(2, 50);
            RuleFor(address => address.Latitude).NotNull();
            RuleFor(address => address.Longitude).NotNull();
        }
    }
}
