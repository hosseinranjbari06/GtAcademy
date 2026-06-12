using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Topics.Commands.EditTopic
{
    public class EditTopicCommandValidation : AbstractValidator<EditTopicDto>
    {
        public EditTopicCommandValidation()
        {
            RuleFor(t => t.Title).NotEmpty().MaximumLength(40);

            RuleFor(t => t.TopicId).NotEmpty();
        }
    }
}
