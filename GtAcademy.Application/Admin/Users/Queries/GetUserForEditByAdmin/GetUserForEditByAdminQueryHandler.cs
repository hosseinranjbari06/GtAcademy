using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Admin.Users.Commands.EditUserByAdmin;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Queries.GetUserForEditByAdmin
{
    public class GetUserForEditByAdminQueryHandler : IRequestHandler<GetUserForEditByAdminQuery, ErrorOr<EditUserDto>>
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public GetUserForEditByAdminQueryHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ErrorOr<EditUserDto>> Handle(GetUserForEditByAdminQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserForEditByAdmin(request.UserId);

            if (user == null) return Error.NotFound();

            var userDto = _mapper.Map<EditUserDto>(user);
            userDto.RoleIds = user.Roles.Select(role => role.RoleId).ToList();

            return userDto;
        }
    }
}
