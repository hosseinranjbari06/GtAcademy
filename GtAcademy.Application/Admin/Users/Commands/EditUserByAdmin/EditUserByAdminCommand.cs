using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Commands.EditUserByAdmin
{
    public record EditUserByAdminCommand(EditUserDto UserDto) : IRequest<ErrorOr<Guid>>;
}
