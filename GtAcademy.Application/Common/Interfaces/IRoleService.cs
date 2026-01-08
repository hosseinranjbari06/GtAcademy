using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Common.Interfaces
{
    public interface IRoleService
    {
        Task<bool> UserHasRole(Guid userId, int roleId);
    }
}
