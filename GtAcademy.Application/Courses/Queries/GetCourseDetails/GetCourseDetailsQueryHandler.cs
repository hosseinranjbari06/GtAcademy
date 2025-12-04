using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Courses.Common;
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

        private readonly IMapper _mapper;

        public GetCourseDetailsQueryHandler(IGenericService<Course> courseGenericService, IMapper mapper)
        {
            _courseGenericService = courseGenericService;
            _mapper = mapper;
        }

        public async Task<ErrorOr<CourseDetailsDto>> Handle(GetCourseDetailsQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseGenericService.GetByIdAsync(request.CourseId);

            if (course == null)
                return Error.NotFound();

            return _mapper.Map<CourseDetailsDto>(course);
        }
    }
}
