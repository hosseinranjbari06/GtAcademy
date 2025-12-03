using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Users;
using GtAcademy.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GtAcademy.Infrastructure.Users.Persistence
{
    public class UserService : IUserService
    {
        private readonly GtAcademyDbContext _context;

        public UserService(GtAcademyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistByPhoneNumber(string phoneNumber)
        {
            return await _context.Users.AnyAsync(user => user.PhoneNumber == phoneNumber);
        }

        public async Task<bool> ExistByUserName(string userName)
        {
            return await _context.Users.AnyAsync(user => user.UserName == userName);
        }

        public async Task<User?> GetUserByPhoneNumber(string phoneNumber)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
        }
    }
}
