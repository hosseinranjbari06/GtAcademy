using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Tools.SmsSender
{
    public interface ISmsSender
    {
        Task<ErrorOr<bool>> SendVerificationCode(string phoneNumber, string code);
    }
}
