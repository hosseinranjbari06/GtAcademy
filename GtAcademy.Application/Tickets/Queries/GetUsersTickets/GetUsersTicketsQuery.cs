using ErrorOr;
using GtAcademy.Application.Forum.Queries.GetForumQuestionsList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Tickets.Queries.GetUsersTickets
{
    public record GetUsersTicketsQuery(Guid UserId) : IRequest<ErrorOr<List<ForumQuestionsListItemDto>>>;
}
