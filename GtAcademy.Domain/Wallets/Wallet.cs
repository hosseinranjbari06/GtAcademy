using GtAcademy.Domain.Common;
using GtAcademy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Domain.Wallets
{
    public class Wallet : BaseDomain
    {
        public Guid WalletId { get; set; }

        public Guid UserId { get; set; }

        public int WalletBalance { get; set; }

        public DateTime LastChargeDate { get; set; }

        public User User { get; set; }
    }
}
