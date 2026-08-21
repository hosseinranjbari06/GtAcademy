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
                .WithName("شماره موبایل")
                .WithMessage("لطفا {PropertyName} را وارد کنید")
                .MinimumLength(11)
                .WithMessage("{PropertyName} نمی تواند کمتر از {MinLength} کاراکتر باشد")
                .MaximumLength(15)
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد");
        }
    }
}
