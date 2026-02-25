using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Users.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Referrals.Queries.GetUserReferralInfo
{
    public class GetUserReferralInfoQueryHandler : IRequestHandler<GetUserReferralInfoQuery, ErrorOr<UserReferralInfoDto>>
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public GetUserReferralInfoQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ErrorOr<UserReferralInfoDto>> Handle(GetUserReferralInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserWithReferralsInfo(request.UserId);

            if (user == null) return Error.NotFound();

            var referralInfo = new UserReferralInfoDto()
            {
                ReferralCode = user.ReferralCode,
                ReferralReceivedUser = _mapper.Map<UserSummaryDto>(user.ReferralReceived?.Referrer),
                ReferralsSent = user.ReferralsSent.Select(_mapper.Map<UsersReferredDto>).ToList()
            };

            return referralInfo;
        }
    }
}
