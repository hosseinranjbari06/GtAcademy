using GtAcademy.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.ReferralOptions.Queries.GetReferralOptions
{
    public class GetReferralOptionsQueryHandler : IRequestHandler<GetReferralOptionsQuery, float>
    {
        private readonly IReferralService _referralService;

        public GetReferralOptionsQueryHandler(IReferralService referralService)
        {
            _referralService = referralService;
        }

        public async Task<float> Handle(GetReferralOptionsQuery request, CancellationToken cancellationToken)
        {
            return await _referralService.GetRewardPercent();
        }
    }
}
