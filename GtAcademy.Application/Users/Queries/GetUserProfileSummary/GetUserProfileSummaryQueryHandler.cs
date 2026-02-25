using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Users.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Queries.GetUserProfileSummary
{
    public class GetUserProfileSummaryQueryHandler : IRequestHandler<GetUserProfileSummaryQuery, ErrorOr<UserSummaryDto>>
    {
        private readonly IUserService _userService;

        public GetUserProfileSummaryQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ErrorOr<UserSummaryDto>> Handle(GetUserProfileSummaryQuery request, CancellationToken cancellationToken)
        {
            var userSummaryDto = await _userService.GetUserSummary(request.UserId);

            if (userSummaryDto == null) return Error.NotFound();

            return userSummaryDto;
        }
    }
}
