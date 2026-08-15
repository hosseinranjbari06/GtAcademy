using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Commands.CreateQuestion
{
    public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionDto>
    {
        public CreateQuestionCommandValidator()
        {
            RuleFor(q => q.Title)
                .NotEmpty()
                .MaximumLength(100)
                .MinimumLength(5);

            RuleFor(q => q.Content)
                .NotEmpty()
                .MaximumLength(10000)
                .MinimumLength(10);

            RuleFor(q => q.UserId)
                .NotEmpty();

            RuleFor(q => q.CourseId)
                .NotEmpty();
        }
    }
}
