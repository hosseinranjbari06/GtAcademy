using FluentValidation;
using GtAcademy.Application.Courses.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseCategories.Commands.CreateCourseCategoryByAdmin
{
    public class CreateCourseCategoryByAdminCommandValidator : AbstractValidator<CourseCategoryDto>
    {
        public CreateCourseCategoryByAdminCommandValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
