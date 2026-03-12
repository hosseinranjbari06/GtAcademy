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

            RuleFor(u => u.UserName)
                .NotEmpty()
                .MaximumLength(255)
                .MinimumLength(2);

            RuleFor(u => u.EmailAddress)
                .EmailAddress()
                .MaximumLength(255)
                .MinimumLength(10);

            RuleFor(u => u.PhoneNumber)
                .NotEmpty()
                .MaximumLength(15)
                .MinimumLength(10);

            RuleFor(u => u.HomeAddress)
                .MaximumLength(500);

            RuleFor(u => u.Biography)
                .MaximumLength(500);

            RuleFor(u => u.Job)
                .MaximumLength(50);

            RuleFor(u => u.ReferralCode)
                .NotEmpty()
                .MaximumLength(20)
                .MinimumLength(5);
        }
    }
}
