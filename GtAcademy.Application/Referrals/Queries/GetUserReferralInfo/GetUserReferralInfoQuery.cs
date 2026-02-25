using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Referrals.Queries.GetUserReferralInfo
{
    public record GetUserReferralInfoQuery(Guid UserId) : IRequest<ErrorOr<UserReferralInfoDto>>;
}
