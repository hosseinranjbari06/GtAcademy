using GtAcademy.Domain.Referral;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Common.Interfaces
{
    public interface IReferralService
    {
        Task<Referral?> GetReferralByReferredId(Guid referredId);

        Task<Guid?> GetUsersReferrerId(Guid userId);

        Task<float> GetRewardPercent();
    }
}
