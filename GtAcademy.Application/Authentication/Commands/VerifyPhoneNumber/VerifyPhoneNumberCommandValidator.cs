using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Authentication.Commands.VerifyPhoneNumber
{
    public class VerifyPhoneNumberCommandValidator : AbstractValidator<VerifyPhoneNumberDto>
    {
        public VerifyPhoneNumberCommandValidator()
        {
            RuleFor(v => v.Code)
                .NotEmpty()
                .WithName("کد")
                .WithMessage("لطفا {PropertyName} را وارد کنید")
                .MaximumLength(5)
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد")
                .MinimumLength(5)
                .WithMessage("{PropertyName} نمی تواند کمتر از {MinLength} کاراکتر باشد");
        }
    }
}
