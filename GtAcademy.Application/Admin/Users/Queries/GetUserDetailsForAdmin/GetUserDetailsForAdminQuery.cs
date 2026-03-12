using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Queries.GetUserDetailsForAdmin
{
    public record GetUserDetailsForAdminQuery(Guid UserId) : IRequest<ErrorOr<UserDetailsDto>>;
}
