using GtAcademy.Application.Common.Interfaces;
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

        public async Task<bool> UserHasRole(Guid userId, int roleId)
        {
            var user = await _context.Users
                .Where(user => user.UserId == userId)
                .Include(user => user.Roles).FirstAsync();

            return user.Roles.Any(role => role.RoleId == roleId);
        }
    }
}
