using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Topics.Commands.DeleteTopic
{
    public record DeleteTopicCommand(int TopicId) : IRequest<ErrorOr<Guid>>;
}
