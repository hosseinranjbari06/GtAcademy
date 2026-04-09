using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Courses.Commands.EditCourseByAdmin
{
    public record EditCourseByAdminCommand(EditCourseDto CourseDto) : IRequest<ErrorOr<Guid>>;
}
