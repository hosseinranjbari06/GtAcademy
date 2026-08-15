using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Queries.CanUserChangeQuestionStatus
{
    public record CanUserChangeQuestionStatusQuery(Guid QuestionId, Guid UserId) : IRequest<ErrorOr<bool>>;
}
