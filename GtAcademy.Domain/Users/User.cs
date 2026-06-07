using GtAcademy.Domain.Common;
using GtAcademy.Domain.Courses;
using GtAcademy.Domain.Orders;
using GtAcademy.Domain.Referral;
using GtAcademy.Domain.Roles;
using GtAcademy.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Domain.Users
{
    public class User : BaseDomain
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string? EmailAddress { get; set; }

        public string? PhoneNumber { get; set; }

        public string AvatarName { get; set; } = string.Empty;

        public string? VerifyToken { get; set; }

        public bool IsActive { get; set; }

        public string? HomeAddress { get; set; }

        public string? Job { get; set; }

        public string? Biography { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime RegisterDate { get; set; }

        public string ReferralCode { get; set; } = string.Empty;

        public Guid? ReferralId { get; set; }

        public Referral.Referral? ReferralReceived { get; set; }

        public List<Referral.Referral> ReferralsSent { get; set; }

        public Wallet Wallet { get; set; }

        public List<Order>? Orders { get; set; }

        public List<Role> Roles { get; set; }
    }
}
