using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Authentication.Commands.LoginWithPhone
{
    public class LoginWithPhoneCommandValidation : AbstractValidator<LoginWithPhoneDto>
    {
        public LoginWithPhoneCommandValidation()
        {
            RuleFor(c => c.PhoneNumber)
                .NotEmpty()
                .MinimumLength(11)
                .MaximumLength(15);
        }
    }
}
