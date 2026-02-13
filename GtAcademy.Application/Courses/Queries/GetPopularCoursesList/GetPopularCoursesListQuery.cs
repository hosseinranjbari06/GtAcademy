using GtAcademy.Application.Courses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Queries.GetPopularCoursesList
{
    public record GetPopularCoursesListQuery(int Take = 3) : IRequest<List<CourseSummaryDto>>;
}
