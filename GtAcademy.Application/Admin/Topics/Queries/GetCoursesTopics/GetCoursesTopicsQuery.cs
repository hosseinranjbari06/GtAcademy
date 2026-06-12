using ErrorOr;
using GtAcademy.Application.Courses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Topics.Queries.GetCoursesTopics
{
    public record GetCoursesTopicsQuery(Guid CourseId) : IRequest<ErrorOr<List<TopicDto>>>;
}
