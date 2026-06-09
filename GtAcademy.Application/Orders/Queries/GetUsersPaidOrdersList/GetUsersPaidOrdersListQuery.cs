using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Orders.Queries.GetUsersPaidOrdersList
{
    public record GetUsersPaidOrdersListQuery(Guid UserId) : IRequest<ErrorOr<List<OrderListItemDto>>>;
}
