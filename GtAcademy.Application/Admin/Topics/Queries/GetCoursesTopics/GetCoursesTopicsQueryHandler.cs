using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Courses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Topics.Queries.GetCoursesTopics
{
    public class GetCoursesTopicsQueryHandler : IRequestHandler<GetCoursesTopicsQuery, ErrorOr<List<TopicDto>>>
    {
        private readonly ITopicService _topicService;

        private readonly IMapper _mapper;

        public GetCoursesTopicsQueryHandler(ITopicService topicService, IMapper mapper)
        {
            _topicService = topicService;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<TopicDto>>> Handle(GetCoursesTopicsQuery request, CancellationToken cancellationToken)
        {
            var topics = await _topicService.GetCoursesTopics(request.CourseId);

            if (topics == null) return Error.NotFound();

            return topics.Select(_mapper.Map<TopicDto>).OrderByDescending(topic => topic.CreateDate).ToList();
        }
    }
}
