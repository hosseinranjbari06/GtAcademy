using FluentValidation;
using GtAcademy.Application.Courses.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseDto>
    {
        public CreateCourseCommandValidator()
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
                .MaximumLength(5000);

            RuleFor(c => c.Tags)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);

            RuleFor(c => c.Price)
                .NotEmpty()
                .LessThan(999999999);
        }
    }
}
