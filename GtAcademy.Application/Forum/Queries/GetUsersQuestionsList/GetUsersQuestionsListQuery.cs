using ErrorOr;
using GtAcademy.Application.Forum.Queries.GetForumQuestionsList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Queries.GetUsersQuestionsList
{
    public record GetUsersQuestionsListQuery(Guid UserId) : IRequest<ErrorOr<List<ForumQuestionsListItemDto>>>;
}
