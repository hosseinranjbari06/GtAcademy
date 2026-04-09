using ErrorOr;
using GtAcademy.Application.Admin.Courses.Commands.CreateCourseByAdmin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Courses.Commands.CreateCourseByAdmin
{
    public record CreateCourseByAdminCommand(CreateCourseDto CourseDto) : IRequest<ErrorOr<Guid>>;
}
