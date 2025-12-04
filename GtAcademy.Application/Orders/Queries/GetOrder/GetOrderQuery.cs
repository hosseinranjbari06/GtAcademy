using ErrorOr;
using GtAcademy.Application.Orders.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Orders.Queries.GetOrder
{
    public record GetOrderQuery(Guid OrderId) : IRequest<ErrorOr<OrderDetailsDto>>;
}
