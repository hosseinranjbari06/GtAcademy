using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Wallets;
using GtAcademy.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Wallets.Persistence
{
    public class WalletService : IWalletService
    {
        private readonly GtAcademyDbContext _context;

        public WalletService(GtAcademyDbContext context)
        {
            _context = context;
        }

        public async Task<Wallet?> GetUsersWalletWithDetails(Guid userId)
        {
            return await _context.Wallets
                .Include(wallet => wallet.WalletIncomes)
                .FirstOrDefaultAsync(wallet => wallet.UserId == userId);
        }
    }
}
