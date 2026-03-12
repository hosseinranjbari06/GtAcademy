using GtAcademy.Domain.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Queries.GetUserDetailsForAdmin
{
    public class UserDetailsDto
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string? EmailAddress { get; set; }

        public string? PhoneNumber { get; set; }

        public string AvatarName { get; set; } = string.Empty;

        public string? VerifyToken { get; set; }

        public bool IsActive { get; set; }

        public string? HomeAddress { get; set; }

        public string? Job { get; set; }

        public string? Biography { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime RegisterDate { get; set; }

        public string ReferralCode { get; set; } = string.Empty;

        public List<Role> Roles { get; set; } = [];
    }
}
