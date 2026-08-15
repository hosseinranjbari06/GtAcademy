using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Commands.DeleteQuestion
{
    public record DeleteQuestionCommand(Guid QuestionId, Guid UserId) : IRequest<ErrorOr<bool>>;
}
