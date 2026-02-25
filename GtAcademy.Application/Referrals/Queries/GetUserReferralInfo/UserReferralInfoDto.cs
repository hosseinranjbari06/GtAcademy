using GtAcademy.Application.Users.Common;
using GtAcademy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Referrals.Queries.GetUserReferralInfo
{
    public class UserReferralInfoDto
    {
        public string ReferralCode { get; set; } = string.Empty;

        public UserSummaryDto? ReferralReceivedUser { get; set; }

        public List<UsersReferredDto> ReferralsSent { get; set; } = [];
    }
}
