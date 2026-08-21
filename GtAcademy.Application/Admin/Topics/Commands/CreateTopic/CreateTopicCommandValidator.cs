using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Topics.Commands.CreateTopic
{
    public class CreateTopicCommandValidator : AbstractValidator<CreateTopicDto>
    {
        public CreateTopicCommandValidator()
        {
            RuleFor(t => t.Title)
            .NotEmpty()
            .WithName("عنوان")
            .WithMessage("لطفا {PropertyName} را وارد کنید")
            .MaximumLength(40)
            .WithMessage("{PropertyName} نمی تواند بیشتر از {MaxLength} کاراکتر باشد");

            RuleFor(t => t.CourseId).NotEmpty();
        }
    }
}
