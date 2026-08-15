using ErrorOr;
using GtAcademy.Application.Forum.Commands.AnswerQuestion;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Tickets.Commands.AnswerTicket
{
    public record AnswerTicketCommand(CreateAnswerDto AnswerDto) : IRequest<ErrorOr<Guid>>;
}
