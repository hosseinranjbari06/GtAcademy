using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Authentication.Commands.RegisterWithPhone
{
    public class RegisterWithPhoneCommandValidator : AbstractValidator<RegisterWithPhoneCommand>
    {
        public RegisterWithPhoneCommandValidator()
        {
            RuleFor(c => c.UserName)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(250);

            RuleFor(c => c.PhoneNumber)
                .NotEmpty()
                .MinimumLength(11)
                .MaximumLength(15);
        }
    }
}
