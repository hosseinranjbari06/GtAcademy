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
                .WithName("عنوان")
                .WithMessage("لطفا {PropertyName} را وارد کنید")
                .MaximumLength(100)
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد")
                .MinimumLength(5)
                .WithMessage("{PropertyName} نمی تواند کمتر از {MinLength} کاراکتر باشد"); ;

            RuleFor(q => q.Content)
                .NotEmpty()
                .WithName("متن سوال")
                .WithMessage("لطفا {PropertyName} را وارد کنید")
                .MaximumLength(10000)
                .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد")
                .MinimumLength(10)
                .WithMessage("{PropertyName} نمی تواند کمتر از {MinLength} کاراکتر باشد"); ;

            RuleFor(q => q.UserId)
                .NotEmpty();

            RuleFor(q => q.CourseId)
                .NotEmpty();
        }
    }
}
