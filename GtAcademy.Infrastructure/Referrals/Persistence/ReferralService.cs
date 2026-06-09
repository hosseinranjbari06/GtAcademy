using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Referral;
using GtAcademy.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GtAcademy.Infrastructure.Referrals.Persistence
{
    public class ReferralService : IReferralService
    {
        private readonly GtAcademyDbContext _context;

        public ReferralService(GtAcademyDbContext context)
        {
            _context = context;
        }

        public async Task<Referral?> GetReferralByReferredId(Guid referredId)
        {
            return await _context.Referrals.FirstOrDefaultAsync(referral => referral.ReferredId == referredId);
        }

        public async Task<float> GetRewardPercent()
        {
            var options = await _context.ReferralOptions.FirstAsync();
            return options.RewardPercent;
        }

        public async Task<Guid?> GetUsersReferrerId(Guid userId)
        {
            var user = await _context.Users.Include(user => user.ReferralReceived).FirstAsync(user => user.UserId == userId);
            return user.ReferralReceived?.ReferrerId;
        }

        public async Task<Guid?> GetUsersReferredId(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user != null ? user.ReferralId : null;
        }
    }
}
