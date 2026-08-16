using GtAcademy.Application.Admin.Users.Queries.GetUsersListForAdmin;
using GtAcademy.Application.Users.Common;
using GtAcademy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<bool> ExistByUserName(string userName);

        Task<bool> ExistById(Guid userId);

        Task<User?> GetUserById(Guid userId);

        Task<bool> ExistByPhoneNumber(string phoneNumber);

        Task<bool> ExistByPhoneNumberIncludeDeletedUsers(string phoneNumber);

        Task<bool> ExistByEmail(string emailAddess); 

        Task<bool> ExistByReferralCode(string referralCode);

        Task<User?> GetUserByPhoneNumber(string phoneNumber);

        Task<User?> GetUserWithReferralsInfo(Guid userId);

        Task<User?> GetUserByReferralCode(string referralCode);

        Task<UserSummaryDto?> GetUserSummary(Guid userId);

        Task<string> GetUserNameById(Guid userName);

        #region Admin

        Task<List<UserListItemDto>> GetUsersListForAdmin(SearchUsersListDto searchDto);

        Task<User?> GetUserWithRelations(Guid userId);

        Task<User?> GetUserByIdForAdmin(Guid userId);

        #endregion
    }
}
