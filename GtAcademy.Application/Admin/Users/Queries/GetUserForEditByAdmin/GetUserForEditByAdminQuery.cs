using ErrorOr;
using GtAcademy.Application.Admin.Users.Commands.EditUserByAdmin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Queries.GetUserForEditByAdmin
{
    public record GetUserForEditByAdminQuery(Guid UserId) : IRequest<ErrorOr<EditUserDto>>;
}
