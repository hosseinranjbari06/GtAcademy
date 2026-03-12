using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Roles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Roles.Queries.GetRolesForAdmin
{
    public class GetRolesForAdminQueryHandler : IRequestHandler<GetRolesForAdminQuery, List<Role>>
    {
        private readonly IGenericService<Role> _genericRoleService;

        public GetRolesForAdminQueryHandler(IGenericService<Role> genericRoleService)
        {
            _genericRoleService = genericRoleService;
        }

        public async Task<List<Role>> Handle(GetRolesForAdminQuery request, CancellationToken cancellationToken)
        {
            var roles = await _genericRoleService.GetAllAsync();
            return roles.ToList();
        }
    }
}
