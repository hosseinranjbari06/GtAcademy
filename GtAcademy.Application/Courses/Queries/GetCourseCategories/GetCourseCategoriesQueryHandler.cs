using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Courses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Queries.GetCourseCategories
{
    public class GetCourseCategoriesQueryHandler : IRequestHandler<GetCourseCategoriesQuery, List<CourseCategoryDto>>
    {
        private readonly ICourseService _courseService;

        public GetCourseCategoriesQueryHandler(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<List<CourseCategoryDto>> Handle(GetCourseCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _courseService.GetCourseCategories();
        }
    }
}
