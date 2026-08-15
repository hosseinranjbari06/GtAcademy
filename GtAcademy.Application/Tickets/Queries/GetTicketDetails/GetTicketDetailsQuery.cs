using ErrorOr;
using GtAcademy.Application.Forum.Queries.GetForumQuestionDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Tickets.Queries.GetTicketDetails
{
    public record GetTicketDetailsQuery(Guid TicketId) : IRequest<ErrorOr<QuestionDetailsDto>>;
}
