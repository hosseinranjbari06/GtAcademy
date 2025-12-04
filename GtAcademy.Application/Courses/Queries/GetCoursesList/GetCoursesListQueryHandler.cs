using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Courses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Queries.GetCoursesList
{
    public class GetCoursesListQueryHandler : IRequestHandler<GetCoursesListQuery, List<CourseSummaryDto>>
    {
        private readonly ICourseService _courseService;

        public GetCoursesListQueryHandler(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<List<CourseSummaryDto>> Handle(GetCoursesListQuery request, CancellationToken cancellationToken)
        {
            return await _courseService.GetCoursesList(request.search, request.seperate, request.pageId);
        }
    }
}
