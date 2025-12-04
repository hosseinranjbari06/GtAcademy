using ErrorOr;
using GtAcademy.Application.Orders.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Orders.Queries.GetUserCurrentOrder
{
    public record GetUserCurrentOrderQuery(Guid UserId) : IRequest<ErrorOr<OrderDetailsDto>>;
}
