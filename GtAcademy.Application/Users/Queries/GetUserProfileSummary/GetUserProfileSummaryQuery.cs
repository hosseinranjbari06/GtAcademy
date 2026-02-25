using ErrorOr;
using GtAcademy.Application.Users.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Queries.GetUserProfileSummary
{
    public record GetUserProfileSummaryQuery(Guid UserId) : IRequest<ErrorOr<UserSummaryDto>>;
}
