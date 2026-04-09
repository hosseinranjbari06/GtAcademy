using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Courses.Queries.GetCoursesListForAdmin
{
    public record GetCoursesListForAdminQuery(SearchCourseListDto SearchDto) : IRequest<List<CourseListItemDto>>;
}
