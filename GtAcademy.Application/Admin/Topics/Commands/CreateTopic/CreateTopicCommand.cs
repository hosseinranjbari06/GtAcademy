using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Topics.Commands.CreateTopic
{
    public record CreateTopicCommand(CreateTopicDto TopicDto) : IRequest<ErrorOr<int>>;
}
