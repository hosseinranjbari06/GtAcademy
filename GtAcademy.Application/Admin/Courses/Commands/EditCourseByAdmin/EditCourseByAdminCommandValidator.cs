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
            .WithName("عنوان")
            .WithMessage("لطفا {PropertyName} را وارد کنید")
            .MinimumLength(5)
            .WithMessage("{PropertyName} نمی تواند کمتر از {MinLength} کاراکتر باشد")
            .MaximumLength(100)
            .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithName("توضیحات")
                .WithMessage("لطفا {PropertyName} را وارد کنید")
                .MinimumLength(20)
                .WithMessage("{PropertyName} نمی تواند کمتر از {MinLength} کاراکتر باشد")
                .MaximumLength(15000)
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد");

            RuleFor(c => c.Tags)
                .NotEmpty()
                .WithName("تگ ها")
                .WithMessage("لطفا {PropertyName} را وارد کنید")
                .MinimumLength(5)
                .WithMessage("{PropertyName} نمی تواند کمتر از {MinLength} کاراکتر باشد")
                .MaximumLength(100)
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد");

            RuleFor(c => c.Price)
                .NotEmpty()
                .WithName("قیمت")
                .WithMessage("لطفا {PropertyName} را وارد کنید")
                .LessThan(999999999)
                .WithMessage("قیمت نمی تواند بیشتر از 999999999 تومان باشد");

            RuleFor(c => c.TeacherId)
                .NotEmpty()
                .WithName("مدرس")
                .WithMessage("لطفا {PropertyName} را انتخاب کنید");

            RuleFor(c => c.CategoryId)
                .NotEmpty()
                .WithName("دسته بندی")
                .WithMessage("لطفا {PropertyName} را انتخاب کنید");
        }
    }
}
