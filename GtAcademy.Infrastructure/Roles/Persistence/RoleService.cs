using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Roles;
using GtAcademy.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Roles.Persistence
{
    public class RoleService : IRoleService
    {
        private readonly GtAcademyDbContext _context;

        public RoleService(GtAcademyDbContext context)
        {
            _context = context;
        }

        public async Task<Role> GetRoleWithUsers(int roleId)
        {
            return await _context.Roles.Include(role => role.Users)
                .FirstAsync(role => role.RoleId == roleId);
        }
    }
}
