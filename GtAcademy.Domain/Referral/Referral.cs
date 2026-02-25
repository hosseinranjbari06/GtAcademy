using GtAcademy.Domain.Common;
using GtAcademy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Domain.Referral
{
    public class Referral : BaseDomain
    {
        public Guid ReferralId { get; set; }

        public Guid ReferrerId { get; set; }

        public Guid ReferredId { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsVerified { get; set; }

        public User Referrer { get; set; } = new User();

        public User Referred { get; set; } = new User();
    }
}
