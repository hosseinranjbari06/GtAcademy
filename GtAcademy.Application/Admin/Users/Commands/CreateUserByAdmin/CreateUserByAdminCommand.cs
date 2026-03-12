using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Commands.CreateUserByAdmin
{
    public record CreateUserByAdminCommand(CreateUserDto UserDto) : IRequest<ErrorOr<Guid>>;
}
