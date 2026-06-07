using GtAcademy.Application.Users.Common;
using GtAcademy.Domain.Users;
using GtAcademy.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Wallets.Queries.GetUsersWalletWithDetails
{
    public class WalletDto
    {
        public Guid WalletId { get; set; }

        public int WalletBalance { get; set; }

        public DateTime? LastChargeDate { get; set; }

        public List<WalletIncomeDto> WalletIncomes { get; set; } = [];
    }
}
