using GtAcademy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<bool> ExistByUserName(string userName);

        Task<bool> ExistByPhoneNumber(string phoneNumber);

        Task<User?> GetUserByPhoneNumber(string phoneNumber);
    }
}
