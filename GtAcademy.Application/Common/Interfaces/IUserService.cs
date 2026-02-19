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

        Task<User?> GetUserById(Guid userId);

        Task<bool> ExistByPhoneNumber(string phoneNumber);

        Task<User?> GetUserByPhoneNumber(string phoneNumber);

        Task<UserSummaryDto?> GetUserSummary(Guid userId);
    }
}
