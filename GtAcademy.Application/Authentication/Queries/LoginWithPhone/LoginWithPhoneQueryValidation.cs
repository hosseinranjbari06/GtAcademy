using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Authentication.Queries.LoginWithPhone
{
    public class LoginWithPhoneQueryValidation : AbstractValidator<LoginWithPhoneQuery>
    {
        public LoginWithPhoneQueryValidation()
        {
            RuleFor(c => c.PhoneNumber)
                .NotEmpty()
                .MinimumLength(11)
                .MaximumLength(15);
        }
    }
}
