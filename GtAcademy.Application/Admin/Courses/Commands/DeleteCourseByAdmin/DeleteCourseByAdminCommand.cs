using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Courses.Commands.DeleteCourseByAdmin
{
    public record DeleteCourseByAdminCommand(Guid CourseId) : IRequest<ErrorOr<bool>>;
}
