using AutoMapper;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Users.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Queries.GetTeachersListForAdmin
{
    public class GetTeachersListForAdminQueryHandler : IRequestHandler<GetTeachersListForAdminQuery, List<UserSummaryDto>>
    {
        private readonly IRoleService _roleService;

        private readonly IMapper _mapper;

        public GetTeachersListForAdminQueryHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<List<UserSummaryDto>> Handle(GetTeachersListForAdminQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleService.GetRoleWithUsers(2);

            return role.Users.Select(_mapper.Map<UserSummaryDto>).ToList();
        }
    }
}
