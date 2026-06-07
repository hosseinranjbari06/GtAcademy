using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Wallets.Queries.GetUsersWalletWithDetails
{
    public class WalletIncomeDto
    {
        public Guid WalletIncomeId { get; set; }

        public int Amount { get; set; }

        public DateTime IncomeDate { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
