using GtAcademy.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Domain.Referral
{
    public class ReferralOptions : BaseDomain
    {
        public int ReferralOptionsId { get; set; }

        public float RewardPercent { get; set; }
    }
}
