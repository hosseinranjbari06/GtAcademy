using GtAcademy.Application.Users.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Referrals.Queries.GetUserReferralInfo
{
    public class UsersReferredDto
    {
        public DateTime CreateDate { get; set; }

        public UserSummaryDto Referred { get; set; } = new UserSummaryDto();
    }
}
