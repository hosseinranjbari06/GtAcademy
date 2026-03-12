using AutoMapper;
using GtAcademy.Application.Admin.Users.Queries.GetUsersListForAdmin;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Users.Common;
using GtAcademy.Domain.Users;
using GtAcademy.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Mail;
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

        public async Task<bool> ExistByEmail(string emailAddess)
        {
            return await _context.Users.AnyAsync(user => user.EmailAddress == emailAddess);
        }

        public async Task<bool> ExistByUserName(string userName)
        {
            return await _context.Users.AnyAsync(user => user.UserName == userName);
        }

        public async Task<bool> ExistByReferralCode(string referralCode)
        {
            return await _context.Users.AnyAsync(user => user.ReferralCode == referralCode);
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

        #region Admin

        public async Task<List<UserListItemDto>> GetUsersListForAdmin(SearchUsersListDto searchDto)
        {
            IQueryable<User> users = _context.Users;

            switch (searchDto.IsActive)
            {
                case "Active":
                    users = users.Where(user => user.IsActive);
                    break;

                case "Deactive":
                    users = users.Where(user => !user.IsActive);
                    break;

                case "All":
                    break;
            }

            if (!string.IsNullOrEmpty(searchDto.UserName)) users = users.Where(user => user.UserName.Contains(searchDto.UserName));
            if (!string.IsNullOrEmpty(searchDto.PhoneNumber)) users = users.Where(user => !string.IsNullOrEmpty(user.PhoneNumber) && user.PhoneNumber.Contains(searchDto.PhoneNumber));
            if (!string.IsNullOrEmpty(searchDto.EmailAddress)) users = users.Where(user => !string.IsNullOrEmpty(user.EmailAddress) && user.EmailAddress.Contains(searchDto.EmailAddress));
            if (!string.IsNullOrEmpty(searchDto.Job)) users = users.Where(user => !string.IsNullOrEmpty(user.Job) && user.Job.Contains(searchDto.Job));
            if (!string.IsNullOrEmpty(searchDto.HomeAddress)) users = users.Where(user => !string.IsNullOrEmpty(user.HomeAddress) && user.HomeAddress.Contains(searchDto.HomeAddress));

            if (searchDto.FromRegisterDate != null) users = users.Where(user => user.RegisterDate >= searchDto.FromRegisterDate);
            if (searchDto.ToRegisterDate != null) users = users.Where(user => user.RegisterDate <= searchDto.ToRegisterDate);

            switch (searchDto.OrderBy)
            {
                case "UserName":
                    users = users.OrderBy(user => user.UserName);
                    break;
                case "RegisterDate":
                    users = users.OrderByDescending(user => user.RegisterDate);
                    break;
                default:
                    users = users.OrderByDescending(user => user.RegisterDate);
                    break;
            }

            users = users.Skip((searchDto.PageId - 1) * searchDto.Take).Take(searchDto.Take);

            return await users.Select(user => _mapper.Map<UserListItemDto>(user)).ToListAsync();
        }

        public async Task<User?> GetUserForEditByAdmin(Guid userId)
        {
            return await _context.Users
                .Where(user => user.UserId == userId)
                .Include(user => user.Roles)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByIdForAdmin(Guid userId)
        {
            return await _context.Users
                .Include(user => user.Roles)
                .FirstOrDefaultAsync(user => user.UserId == userId);
        }

        #endregion
    }
}
