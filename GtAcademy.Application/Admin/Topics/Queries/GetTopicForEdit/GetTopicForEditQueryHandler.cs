using ErrorOr;
using GtAcademy.Application.Admin.Topics.Commands.EditTopic;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Topics.Queries.GetTopicForEdit
{
    public class GetTopicForEditQueryHandler : IRequestHandler<GetTopicForEditQuery, ErrorOr<EditTopicDto>>
    {
        private readonly IGenericService<Topic> _genericTopicService;

        public GetTopicForEditQueryHandler(IGenericService<Topic> genericTopicService)
        {
            _genericTopicService = genericTopicService;
        }

        public async Task<ErrorOr<EditTopicDto>> Handle(GetTopicForEditQuery request, CancellationToken cancellationToken)
        {
            var topic = await _genericTopicService.GetByIdAsync(request.TopicId);

            if (topic == null) return Error.NotFound();

            return new EditTopicDto(){ TopicId = topic.TopicId, Title = topic.Title, CourseId = topic.CourseId };
        }
    }
}
