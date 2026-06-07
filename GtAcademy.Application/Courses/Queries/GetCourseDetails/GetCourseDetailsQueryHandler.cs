using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Application.Users.Common;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Queries.GetCourseDetails
{
    public class GetCourseDetailsQueryHandler : IRequestHandler<GetCourseDetailsQuery, ErrorOr<CourseDetailsDto>>
    {
        private readonly ICourseService _courseService;

        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public GetCourseDetailsQueryHandler(IMapper mapper, IUserService userService, ICourseService courseService)
        {
            _mapper = mapper;
            _userService = userService;
            _courseService = courseService;
        }

        public async Task<ErrorOr<CourseDetailsDto>> Handle(GetCourseDetailsQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseService.GetCourseWithEpisodes(request.CourseId);

            if (course == null)
                return Error.NotFound();

            var courseDto = _mapper.Map<Course, CourseDetailsDto>(course);
            var userSummary = await _userService.GetUserSummary(course.TeacherId);

            if (userSummary == null)
                return Error.NotFound();

            courseDto.TeacherSummary = userSummary;

            courseDto.CourseComments.ForEach(async comment => comment.User = await _userService.GetUserSummary(comment.UserId));

            courseDto.Topics.ForEach(topic => { courseDto.EpisodeCount += topic.Episodes.Count; });

            return courseDto;
        }
    }
}
