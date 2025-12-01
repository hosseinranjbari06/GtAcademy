using GtAcademy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Domain.Wallets
{
    public class Wallet
    {
        public Guid WalletId { get; set; }

        public Guid UserId { get; set; }

        public int WalletBalance { get; set; }

        public DateTime LastChargeDate { get; set; }

        public User User { get; set; }
    }
}
