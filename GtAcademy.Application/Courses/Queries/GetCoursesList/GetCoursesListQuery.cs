using GtAcademy.Application.Courses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Queries.GetCoursesList
{
    public record GetCoursesListQuery(
                    string search = "",
                    int seperate = 6,
                    int pageId = 1) : IRequest<List<CourseSummaryDto>>;
}
