using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseComments.Commands.SubmitCourseCommentByAdmin
{
    public record SubmitCourseCommentByAdminCommand(Guid CommentId) : IRequest<ErrorOr<bool>>;
}
