using GtAcademy.Domain.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Common.Interfaces
{
    public interface IRoleService
    {
        Task<Role> GetRoleWithUsers(int roleId);
    }
}
