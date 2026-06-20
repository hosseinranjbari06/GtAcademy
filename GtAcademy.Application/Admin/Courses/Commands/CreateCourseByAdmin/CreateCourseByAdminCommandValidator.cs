using FluentValidation;
using GtAcademy.Application.Admin.Courses.Commands.CreateCourseByAdmin;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Courses.Commands.CreateCourseByAdmin
{
    public class CreateCourseByAdminCommandValidator : AbstractValidator<CreateCourseDto>
    {
        public CreateCourseByAdminCommandValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);

            RuleFor(c => c.BannerName)
                .NotEmpty();

            RuleFor(c => c.Description)
                .NotEmpty()
                .MinimumLength(20)
                .MaximumLength(15000);

            RuleFor(c => c.Tags)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);

            RuleFor(c => c.Price)
                .NotEmpty()
                .LessThan(999999999);

            RuleFor(c => c.TeacherId)
                .NotEmpty();

            RuleFor(c => c.CategoryId)
                .NotEmpty();
        }
    }
}
