using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Queries.GetUsersListForAdmin
{
    public class UserListItemDto
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string? EmailAddress { get; set; }

        public string? PhoneNumber { get; set; }

        public string AvatarName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}
