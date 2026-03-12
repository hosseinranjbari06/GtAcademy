using GtAcademy.Domain.Common;
using GtAcademy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace GtAcademy.Domain.Roles
{
    public class Role : BaseDomain
    {
        public int RoleId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<User> Users { get; set; } = new List<User>();
    }
}
