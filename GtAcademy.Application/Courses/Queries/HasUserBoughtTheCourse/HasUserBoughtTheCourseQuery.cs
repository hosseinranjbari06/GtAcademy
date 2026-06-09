using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Queries.HasUserBoughtTheCourse
{
    public record HasUserBoughtTheCourseQuery(Guid UserId, Guid CourseId) : IRequest<ErrorOr<bool>>;
}
