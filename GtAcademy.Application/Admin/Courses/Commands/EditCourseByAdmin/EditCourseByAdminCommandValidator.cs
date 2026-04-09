using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Courses.Commands.EditCourseByAdmin
{
    public class EditCourseByAdminCommandValidator : AbstractValidator<EditCourseDto>
    {
        public EditCourseByAdminCommandValidator()
        {
            RuleFor(c => c.CourseId)
                .NotEmpty();

            RuleFor(c => c.Title)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);

            RuleFor(c => c.Description)
                .NotEmpty()
                .MinimumLength(20)
                .MaximumLength(5000);

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
