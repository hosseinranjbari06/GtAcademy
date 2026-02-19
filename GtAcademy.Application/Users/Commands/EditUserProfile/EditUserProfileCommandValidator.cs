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
                .MaximumLength(255)
                .MinimumLength(2);

            RuleFor(p => p.HomeAddress)
                .MaximumLength(500);

            RuleFor(p => p.Biography)
                .MaximumLength(500);

            RuleFor(p => p.Job)
                .MaximumLength(50);
        }
    }
}
