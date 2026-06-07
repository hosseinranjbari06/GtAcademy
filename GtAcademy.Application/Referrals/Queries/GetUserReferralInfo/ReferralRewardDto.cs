using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Referrals.Queries.GetUserReferralInfo
{
    public class ReferralRewardDto
    {
        public int Amount { get; set; }

        public DateTime IncomeDate { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
