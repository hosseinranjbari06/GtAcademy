using ErrorOr;
using GtAcademy.Application.Users.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Queries.GetUserProfileForEdit
{
    public record GetUserProfileForEditQuery(Guid UserId) : IRequest<ErrorOr<EditUserProfileDto>>;
}
