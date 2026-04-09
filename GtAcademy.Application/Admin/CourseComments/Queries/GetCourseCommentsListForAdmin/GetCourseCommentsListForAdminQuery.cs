using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseComments.Queries.GetCourseCommentsListForAdmin
{
    public record GetCourseCommentsListForAdminQuery(SearchCourseCommentsListDto SearchDto) : IRequest<SearchCourseCommentsListDto>;
}
