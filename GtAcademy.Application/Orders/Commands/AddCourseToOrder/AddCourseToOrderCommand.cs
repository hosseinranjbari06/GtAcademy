using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Orders.Commands.AddCourseToOrder
{
    public record AddCourseToOrderCommand(Guid UserId, Guid CourseId) : IRequest<ErrorOr<bool>>;
}
