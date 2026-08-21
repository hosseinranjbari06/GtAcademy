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
                .WithName("عنوان")
                .WithMessage("لطفا {PropertyName} را وارد کنید")
                .MaximumLength(50)
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد");
        }
    }
}
