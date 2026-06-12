using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Topics.Commands.EditTopic
{
    public record EditTopicCommand(EditTopicDto TopicDto) : IRequest<ErrorOr<int>>;
}
