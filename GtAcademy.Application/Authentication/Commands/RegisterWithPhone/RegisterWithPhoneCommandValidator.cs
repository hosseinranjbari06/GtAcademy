using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Authentication.Commands.RegisterWithPhone
{
    public class RegisterWithPhoneCommandValidator : AbstractValidator<RegisterWithPhoneDto>
    {
        public RegisterWithPhoneCommandValidator()
        {
            RuleFor(c => c.UserName)
                .NotEmpty()
                .WithName("نام کاربری")
                .WithMessage("لطفا {PropertyName} را وارد کنید")
                .MinimumLength(5)
                .WithMessage("{PropertyName} نمی تواند کمتر از {MinLength} کاراکتر باشد")
                .MaximumLength(250)
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد");

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
