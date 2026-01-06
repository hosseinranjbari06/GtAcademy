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
        private readonly IGenericService<Course> _courseGenericService;

        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public GetCourseDetailsQueryHandler(IGenericService<Course> courseGenericService, IMapper mapper, IUserService userService)
        {
            _courseGenericService = courseGenericService;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ErrorOr<CourseDetailsDto>> Handle(GetCourseDetailsQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseGenericService.GetByIdAsync(request.CourseId);

            if (course == null)
                return Error.NotFound();

            var courseDto = _mapper.Map<CourseDetailsDto>(course);
            var userSummary = await _userService.GetUserSummary(course.TeacherId);

            if(userSummary == null) return Error.NotFound();

            courseDto.TeacherSummary = userSummary;

            return courseDto;
        }
    }
}
