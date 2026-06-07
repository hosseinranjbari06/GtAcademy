using GtAcademy.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Domain.Wallets
{
    public class WalletIncome : BaseDomain
    {
        public Guid WalletIncomeId { get; set; }

        public int Amount { get; set; }

        public DateTime IncomeDate { get; set; }

        public string Description { get; set; } = string.Empty;

        public Guid WalletId { get; set; }

        public bool IsPaid { get; set; }

        public bool IsReferralReward { get; set; }

        public Guid? ReferredId { get; set; }

        public Wallet Wallet { get; set; } = new Wallet();
    }
}
