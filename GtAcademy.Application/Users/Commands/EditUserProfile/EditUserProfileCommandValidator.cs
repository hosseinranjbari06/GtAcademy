using FluentValidation;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Application.Users.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Commands.EditUserProfile
{
    public class EditUserProfileCommandValidator : AbstractValidator<EditUserProfileDto>
    {
        public EditUserProfileCommandValidator()
        {
            RuleFor(p => p.UserId)
                .NotEmpty();

            RuleFor(p => p.UserName)
                .NotEmpty()
                .WithName("نام کاربری")
                .WithMessage("لطفا {PropertyName} را وارد کنید")
                .MaximumLength(255)
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد")
                .MinimumLength(2)
                .WithMessage("{PropertyName} نمی تواند کمتر از {MinLength} کاراکتر باشد");

            RuleFor(p => p.HomeAddress)
                .MaximumLength(500)
                .WithName("آدرس")
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد");

            RuleFor(p => p.Biography)
                .MaximumLength(500)
                .WithName("بیوگرافی")
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد");

            RuleFor(p => p.Job)
                .MaximumLength(50)
                .WithName("شغل")
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد");
        }
    }
}
