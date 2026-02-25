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
    }
}
