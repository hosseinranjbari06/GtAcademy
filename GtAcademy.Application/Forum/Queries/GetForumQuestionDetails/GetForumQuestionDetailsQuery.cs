using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Queries.GetForumQuestionDetails
{
    public record GetForumQuestionDetailsQuery(Guid QuestionId) : IRequest<ErrorOr<QuestionDetailsDto>>;
}
