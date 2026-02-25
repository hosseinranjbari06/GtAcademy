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

        public async Task<User?> GetUserById(Guid userId)
        {
            return await _context.Users
                .Where(u => u.UserId == userId)
                .Include(u => u.Orders)
                .Include(u => u.Wallet)
                .Include(u => u.Roles)
                .Include(u => u.Comments)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByPhoneNumber(string phoneNumber)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
        }

        public async Task<User?> GetUserByReferralCode(string referralCode)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.ReferralCode == referralCode);
        }

        public async Task<UserSummaryDto?> GetUserSummary(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserId == userId);
            return _mapper.Map<UserSummaryDto>(user);
        }

        public async Task<User?> GetUserWithReferralsInfo(Guid userId)
        {
            return await _context.Users
                .Where(user => user.UserId == userId)
                .Include(user => user.ReferralReceived)
                .ThenInclude(referral => referral.Referrer)
                .Include(user => user.ReferralsSent)
                .ThenInclude(referral => referral.Referred)
                .FirstOrDefaultAsync();
        }
    }
}
