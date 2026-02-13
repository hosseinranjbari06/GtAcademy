using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Courses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Queries.GetPopularCoursesList
{
    public class GetPopularCoursesListQueryHandler : IRequestHandler<GetPopularCoursesListQuery, List<CourseSummaryDto>>
    {
        private readonly ICourseService _courseService;

        public GetPopularCoursesListQueryHandler(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<List<CourseSummaryDto>> Handle(GetPopularCoursesListQuery request, CancellationToken cancellationToken)
        {
            return await _courseService.GetPopularCoursesList(request.Take);
        }
    }
}
