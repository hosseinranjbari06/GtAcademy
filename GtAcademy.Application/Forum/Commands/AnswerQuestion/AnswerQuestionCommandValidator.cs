using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Commands.AnswerQuestion
{
    public class AnswerQuestionCommandValidator : AbstractValidator<CreateAnswerDto>
    {
        public AnswerQuestionCommandValidator()
        {
            RuleFor(a => a.Content)
                .NotEmpty()
                .MaximumLength(10000)
                .MinimumLength(10);

            RuleFor(q => q.UserId)
                .NotEmpty();

            RuleFor(q => q.QuestionId)
                .NotEmpty();
        }
    }
}
