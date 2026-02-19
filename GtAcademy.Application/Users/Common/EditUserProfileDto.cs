using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Common
{
    public class EditUserProfileDto
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string? AvatarName { get; set; }

        public string? HomeAddress { get; set; }

        public string? Job { get; set; }

        public string? Biography { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
