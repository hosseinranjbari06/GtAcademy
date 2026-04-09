using AutoMapper;
using GtAcademy.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Courses.Queries.GetCoursesListForAdmin
{
    public class GetCoursesListForAdminQueryHandler : IRequestHandler<GetCoursesListForAdminQuery, List<CourseListItemDto>>
    {
        private readonly ICourseService _courseService;

        public GetCoursesListForAdminQueryHandler(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<List<CourseListItemDto>> Handle(GetCoursesListForAdminQuery request, CancellationToken cancellationToken)
        {
            return await _courseService.GetCoursesListForAdmin(request.SearchDto);
        }
    }
}
