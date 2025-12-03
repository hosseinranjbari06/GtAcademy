using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Tools.SMS_Sender
{
    public interface ISmsSender
    {
        ErrorOr<bool> SendPublicSms(string receiver, string body);

        ErrorOr<bool> SendLookUpSms(string receiver, string body);
    }
}
