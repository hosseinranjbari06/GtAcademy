using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Permissions.Persistence
{
    public class PermissionService : IPermissionService
    {
        private readonly GtAcademyDbContext _context;

        public PermissionService(GtAcademyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserHasAnyRole(Guid userId)
        {
            var user = await _context.Users
                .Where(user => user.UserId == userId)
                .Include(user => user.Roles).FirstAsync();

            return user.Roles.Any();
        }

        public async Task<bool> UserHasRole(Guid userId, int roleId)
        {
            var user = await _context.Users
                .Include(user => user.Roles)
                .FirstAsync(user => user.UserId == userId);

            return user.Roles.Any(role => role.RoleId == roleId);
        }

        public void DisposeContext()
        {
            _context.Dispose();
        }
    }
}
