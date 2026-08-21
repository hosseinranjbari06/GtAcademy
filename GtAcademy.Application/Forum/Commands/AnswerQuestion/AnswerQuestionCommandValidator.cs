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
                .WithName("متن پاسخ")
                .WithMessage("لطفا {PropertyName} را وارد کنید")
                .MaximumLength(10000)
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد")
                .MinimumLength(10)
                .WithMessage("{PropertyName} نمی تواند کمتر از {MinLength} کاراکتر باشد"); ;

            RuleFor(q => q.UserId)
                .NotEmpty();

            RuleFor(q => q.QuestionId)
                .NotEmpty();
        }
    }
}
