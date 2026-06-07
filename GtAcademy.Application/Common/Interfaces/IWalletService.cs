using GtAcademy.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Common.Interfaces
{
    public interface IWalletService
    {
        Task<Wallet?> GetUsersWalletWithDetails(Guid userId);
    }
}
