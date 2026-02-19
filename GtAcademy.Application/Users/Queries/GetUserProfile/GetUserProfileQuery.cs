using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Queries.GetUserProfile
{
    public record GetUserProfileQuery(Guid UserId) : IRequest<ErrorOr<UserProfileDto>>;
}
