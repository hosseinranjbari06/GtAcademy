using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Queries.GetUsersListForAdmin
{
    public class SearchUsersListDto
    {
        public string? UserName { get; set; }

        public string? EmailAddress { get; set; }

        public string? PhoneNumber { get; set; }

        public string IsActive { get; set; } = string.Empty;

        public string? HomeAddress { get; set; }

        public string? Job { get; set; }

        public DateTime? FromRegisterDate { get; set; }

        public DateTime? ToRegisterDate { get; set; }

        public string? OrderBy { get; set; }

        public int PageId { get; set; } = 1;

        public int Take { get; set; } = 50;
    }
}
