using GtAcademy.Application.Forum.Queries.GetForumQuestionsList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Tickets.Queries.GetTicketsList
{
    public record GetTicketsListQuery(ForumQuestionsSearchDto SearchDto) : IRequest<List<ForumQuestionsListItemDto>>;
}
