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
            RuleFor(t => t.Title).NotEmpty().MaximumLength(40);

            RuleFor(t => t.CourseId).NotEmpty();
        }
    }
}
