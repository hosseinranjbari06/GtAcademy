using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Commands.EditUserByAdmin
{
    public class EditUserDto
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string? EmailAddress { get; set; }

        public string? PhoneNumber { get; set; }

        public string? HomeAddress { get; set; }

        public string? Job { get; set; }

        public string? Biography { get; set; }

        public DateTime? BirthDate { get; set; }

        public string ReferralCode { get; set; } = string.Empty;

        public bool DeleteAvatar { get; set; }

        public List<int> RoleIds { get; set; } = [];
    }
}
