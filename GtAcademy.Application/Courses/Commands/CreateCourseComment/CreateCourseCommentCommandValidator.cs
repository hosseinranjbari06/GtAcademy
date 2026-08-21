using FluentValidation;
using GtAcademy.Application.Courses.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Commands.CreateCourseComment
{
    public class CreateCourseCommentCommandValidator : AbstractValidator<CreateCourseCommentDto>
    {
        public CreateCourseCommentCommandValidator()
        {
            RuleFor(cc => cc.CourseId)
                .NotEmpty()
                .WithMessage("درخواست شما نامعتبر است");

            RuleFor(cc => cc.UserId)
                .NotEmpty();

            RuleFor(cc => cc.Content)
                .NotEmpty()
                .WithName("متن نظر")
                .WithMessage("لطفا {PropertyName} را وارد کنید")
                .MaximumLength(500)
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد");
        }
    }
}
