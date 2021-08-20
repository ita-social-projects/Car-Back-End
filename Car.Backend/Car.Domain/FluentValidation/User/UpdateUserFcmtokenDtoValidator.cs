using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Data.Constants;
using Car.Domain.Dto;
using FluentValidation;

namespace Car.Domain.FluentValidation.User
{
    public class UpdateUserFcmtokenDtoValidator : AbstractValidator<UpdateUserFcmtokenDto>
    {
        public UpdateUserFcmtokenDtoValidator()
        {
            RuleFor(user => user.Id).GreaterThan(Constants.IdLength);
        }
    }
}
