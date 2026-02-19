using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Queries.GetUserProfile
{
    public class UserProfileDto
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string? EmailAddress { get; set; }

        public string? PhoneNumber { get; set; }

        public string AvatarName { get; set; } = string.Empty;

        public string? HomeAddress { get; set; }

        public string? Job { get; set; }

        public string? Biography { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
