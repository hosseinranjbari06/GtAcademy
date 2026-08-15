using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Commands.CloseQuestion
{
    public record CloseQuestionCommand(Guid QuestionId, Guid UserId) : IRequest<ErrorOr<Guid>>;
}
