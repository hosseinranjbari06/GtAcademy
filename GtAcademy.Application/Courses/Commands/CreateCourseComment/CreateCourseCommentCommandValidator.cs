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
                .NotEmpty();

            RuleFor(cc => cc.UserId)
                .NotEmpty();

            RuleFor(cc => cc.Content)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
