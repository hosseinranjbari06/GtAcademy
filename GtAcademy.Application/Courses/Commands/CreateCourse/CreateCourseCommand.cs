using ErrorOr;
using GtAcademy.Application.Courses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Commands.CreateCourse
{
    public record CreateCourseCommand(CreateCourseDto CourseDto) : IRequest<ErrorOr<Guid>>;
}
