using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Queries.IsUserAdmin
{
    public record IsUserAdminQuery(Guid UserId) : IRequest<ErrorOr<bool>>;
}
