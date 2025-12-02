using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Authentication.Common
{
    public class AuthenticationResult
    {
        public Guid UserId { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;
    }
}
