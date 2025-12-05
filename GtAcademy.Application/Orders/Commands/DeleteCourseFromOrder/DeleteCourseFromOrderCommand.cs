using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Orders.Commands.DeleteCourseFromOrder
{
    public record DeleteCourseFromOrderCommand(Guid UserId, Guid CourseId) : IRequest<ErrorOr<bool>>;
}
