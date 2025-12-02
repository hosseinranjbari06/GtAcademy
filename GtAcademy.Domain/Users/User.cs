using GtAcademy.Domain.Common;
using GtAcademy.Domain.Courses;
using GtAcademy.Domain.Orders;
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

        public string? VerifyToken { get; set; }

        public bool IsActive { get; set; }

        public string? HomeAddress { get; set; }

        public string? Job { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime RegisterDate { get; set; }

        public Wallet Wallet { get; set; }

        public List<Order>? Orders { get; set; }
    }
}
