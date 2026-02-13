using ErrorOr;
using GtAcademy.Application.Courses.Commands.CreateCourseComment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Commands.CreateComment
{
    public record CreateCourseCommentCommand(CreateCourseCommentDto CommentDto) : IRequest<ErrorOr<Guid>>;
}
