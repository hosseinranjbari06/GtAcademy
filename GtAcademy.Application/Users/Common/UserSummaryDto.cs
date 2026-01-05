using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Common
{
    public class UserSummaryDto
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string AvatarName { get; set; }

        public string? Biography { get; set; }
    }
}
