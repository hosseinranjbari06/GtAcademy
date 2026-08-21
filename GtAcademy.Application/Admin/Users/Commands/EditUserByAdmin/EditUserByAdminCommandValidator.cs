using FluentValidation;
using GtAcademy.Application.Users.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Commands.EditUserByAdmin
{
    public class EditUserByAdminCommandValidator : AbstractValidator<EditUserDto>
    {
        public EditUserByAdminCommandValidator()
        {
            RuleFor(u => u.UserId)
                .NotEmpty();

            RuleFor(u => u.ReferralCode)
                .NotEmpty()
                .WithName("کد معرفی")
                .WithMessage("لطفا {PropertyName} را وارد کنید")
                .MaximumLength(20)
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد")
                .MinimumLength(5)
                .WithMessage("{PropertyName} نمی تواند کمتر از {MinLength} کاراکتر باشد");

            RuleFor(u => u.UserName)
                .NotEmpty()
                .WithName("نام کاربری")
                .WithMessage("لطفا {PropertyName} را وارد کنید")
                .MaximumLength(255)
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد")
                .MinimumLength(2)
                .WithMessage("{PropertyName} نمی تواند کمتر از {MinLength} کاراکتر باشد");

            RuleFor(u => u.EmailAddress)
                .EmailAddress()
                .WithName("ایمیل")
                .MaximumLength(255)
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد")
                .MinimumLength(10)
                .WithMessage("{PropertyName} نمی تواند کمتر از {MinLength} کاراکتر باشد");

            RuleFor(u => u.PhoneNumber)
                .NotEmpty()
                .WithName("شماره موبایل")
                .WithMessage("لطفا {PropertyName} را وارد کنید")
                .MaximumLength(15)
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد")
                .MinimumLength(10)
                .WithMessage("{PropertyName} نمی تواند کمتر از {MinLength} کاراکتر باشد");

            RuleFor(u => u.HomeAddress)
                .MaximumLength(500)
                .WithName("آدرس")
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد");

            RuleFor(u => u.Biography)
                .MaximumLength(500)
                .WithName("بیوگرافی")
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد");

            RuleFor(u => u.Job)
                .MaximumLength(50)
                .WithName("شغل")
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد"); ;
        }
    }
}
