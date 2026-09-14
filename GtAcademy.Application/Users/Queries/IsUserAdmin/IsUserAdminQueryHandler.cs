using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Queries.IsUserAdmin
{
    public class IsUserAdminQueryHandler : IRequestHandler<IsUserAdminQuery, ErrorOr<bool>>
    {
        private readonly IUserService _userService;

        public IsUserAdminQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ErrorOr<bool>> Handle(IsUserAdminQuery request, CancellationToken cancellationToken)
        {
            if (!await _userService.ExistById(request.UserId)) return Error.NotFound();

            return await _userService.IsUserAdmin(request.UserId);
        }
    }
}
