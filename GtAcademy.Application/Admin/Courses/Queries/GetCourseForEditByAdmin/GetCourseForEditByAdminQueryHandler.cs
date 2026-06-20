using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Admin.Courses.Commands.EditCourseByAdmin;
using GtAcademy.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Courses.Queries.GetCourseForEditByAdmin
{
    public class GetCourseForEditByAdminQueryHandler : IRequestHandler<GetCourseForEditByAdminQuery, ErrorOr<EditCourseDto>>
    {
        private readonly ICourseService _courseService;

        private readonly IMapper _mapper;

        public GetCourseForEditByAdminQueryHandler(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        public async Task<ErrorOr<EditCourseDto>> Handle(GetCourseForEditByAdminQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseService.GetCourseWithRelations(request.CourseId);

            if (course == null) return Error.NotFound();

            EditCourseDto courseDto = _mapper.Map<EditCourseDto>(course);
            if (course.CourseCategories.Any())
            {
                courseDto.CategoryId = course.CourseCategories.First().CategoryId;
            }
            else
            {
                courseDto.CategoryId = Guid.Empty;
            }

            return courseDto;
        }
    }
}
