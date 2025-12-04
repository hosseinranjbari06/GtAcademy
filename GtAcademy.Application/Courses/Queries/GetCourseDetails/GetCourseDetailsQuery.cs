using ErrorOr;
using GtAcademy.Application.Courses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Queries.GetCourseDetails
{
    public record GetCourseDetailsQuery(Guid CourseId) : IRequest<ErrorOr<CourseDetailsDto>>;
}
