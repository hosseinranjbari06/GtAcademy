using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Tickets.Queries.GetTicketsCount
{
    public record GetTicketsCountQuery() : IRequest<int>;
}
