using ErrorOr;
using GtAcademy.Application.Admin.Topics.Commands.EditTopic;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Topics.Queries.GetTopicForEdit
{
    public record GetTopicForEditQuery(int TopicId) : IRequest<ErrorOr<EditTopicDto>>;
}
