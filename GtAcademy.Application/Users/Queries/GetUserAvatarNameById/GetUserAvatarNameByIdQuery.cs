using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Queries.GetUserAvatarNameById
{
    public record GetUserAvatarNameByIdQuery(Guid UserId) : IRequest<ErrorOr<string>>;
}
