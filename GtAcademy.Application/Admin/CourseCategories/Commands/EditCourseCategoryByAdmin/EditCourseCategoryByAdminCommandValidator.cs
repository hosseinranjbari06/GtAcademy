using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseCategories.Commands.EditCourseCategoryByAdmin
{
    public class EditCourseCategoryByAdminCommandValidator : AbstractValidator<EditCourseCategoryByAdminCommand>
    {
        public EditCourseCategoryByAdminCommandValidator()
        {
            RuleFor(c => c.CategoryDto.CategoryId)
                .NotEmpty();

            RuleFor(c => c.CategoryDto.Title)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
