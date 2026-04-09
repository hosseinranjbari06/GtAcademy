using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseComments.Commands.DeleteCourseCommentByAdmin
{
    public record DeleteCourseCommentByAdminCommand(Guid CommentId) : IRequest<ErrorOr<bool>>;
}
