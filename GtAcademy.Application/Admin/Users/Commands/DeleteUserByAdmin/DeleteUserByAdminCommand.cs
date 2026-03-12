using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Commands.DeleteUserByAdmin
{
    public record DeleteUserByAdminCommand(Guid UserId) : IRequest<ErrorOr<bool>>;
}
