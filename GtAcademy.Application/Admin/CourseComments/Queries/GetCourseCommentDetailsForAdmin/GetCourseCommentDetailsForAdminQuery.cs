using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseComments.Queries.GetCourseCommentDetailsForAdmin
{
    public record GetCourseCommentDetailsForAdminQuery(Guid CommentId) : IRequest<ErrorOr<CourseCommentDetailsDto>>;
}
