using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Orders.Commands.OrderPayment
{
    public record OrderPaymentCommand(Guid UserId) : IRequest<ErrorOr<Guid>>;
}
