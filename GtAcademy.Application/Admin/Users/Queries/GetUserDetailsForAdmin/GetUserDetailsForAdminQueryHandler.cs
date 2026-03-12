using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Queries.GetUserDetailsForAdmin
{
    public class GetUserDetailsForAdminQueryHandler : IRequestHandler<GetUserDetailsForAdminQuery, ErrorOr<UserDetailsDto>>
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public GetUserDetailsForAdminQueryHandler(IMapper mapper, IUserService userService)
        {

            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ErrorOr<UserDetailsDto>> Handle(GetUserDetailsForAdminQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdForAdmin(request.UserId);

            if (user == null) return Error.NotFound();

            return _mapper.Map<UserDetailsDto>(user);
        }
    }
}
