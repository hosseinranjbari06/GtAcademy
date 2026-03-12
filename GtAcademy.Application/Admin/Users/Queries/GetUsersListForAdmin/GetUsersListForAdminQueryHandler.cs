using GtAcademy.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Queries.GetUsersListForAdmin
{
    public class GetUsersListForAdminQueryHandler : IRequestHandler<GetUsersListForAdminQuery, List<UserListItemDto>>
    {
        private readonly IUserService _userService;

        public GetUsersListForAdminQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<UserListItemDto>> Handle(GetUsersListForAdminQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUsersListForAdmin(request.SearchDto);
        }
    }
}
