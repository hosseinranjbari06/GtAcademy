using ErrorOr;
using GtAcademy.Application.Admin.Courses.Commands.EditCourseByAdmin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Courses.Queries.GetCourseForEditByAdmin
{
    public record GetCourseForEditByAdminQuery(Guid CourseId) : IRequest<ErrorOr<EditCourseDto>>;
}
