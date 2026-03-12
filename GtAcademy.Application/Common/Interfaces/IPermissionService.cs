using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Common.Interfaces
{
    public interface IPermissionService
    {
        Task<bool> UserHasRole(Guid userId, int roleId);

        Task<bool> UserHasAnyRole(Guid userId);

        void DisposeContext();
    }
}
