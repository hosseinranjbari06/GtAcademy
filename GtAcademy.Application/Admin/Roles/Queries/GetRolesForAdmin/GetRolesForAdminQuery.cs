using GtAcademy.Domain.Roles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Roles.Queries.GetRolesForAdmin
{
    public record GetRolesForAdminQuery() : IRequest<List<Role>>;
}
