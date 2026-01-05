using AutoMapper;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Users.Common;
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
        private readonly IMapper _mapper;

        public UserService(GtAcademyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<UserSummaryDto?> GetUserSummary(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserId == userId);
            return _mapper.Map<UserSummaryDto>(user);
        }
    }
}
