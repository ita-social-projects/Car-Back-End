using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Car.Data.FluentValidation
{
    public class CarValidator : AbstractValidator<Entities.Car>
    {
        public CarValidator()
        {
            RuleFor(car => car.Id).GreaterThan(0);
            RuleFor(car => car.Brand).NotNull().MinimumLength(2).MaximumLength(25);
            RuleFor(car => car.Model).NotNull().MinimumLength(2).MaximumLength(25);
            RuleFor(car => car.Color).NotNull().Length(2, 25);
            RuleFor(car => car.PlateNumber).NotNull().Length(4, 10);
            RuleFor(car => car.UserId).GreaterThan(0);
            RuleFor(car => car.Owner).SetValidator(new UserValidator());
        }
    }
}
