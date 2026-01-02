using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Authentication.Commands.VerifyPhoneNumber
{
    public class VerifyPhoneNumberDto
    {
        public string PhoneNumber { get; set; }

        public string Code { get; set; }
    }
}
