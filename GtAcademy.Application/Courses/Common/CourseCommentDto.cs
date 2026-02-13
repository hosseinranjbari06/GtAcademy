using GtAcademy.Application.Users.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Common
{
    public class CourseCommentDto
    {
        public Guid CommentId { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; }

        public Guid UserId { get; set; }

        public UserSummaryDto User { get; set; } = new UserSummaryDto();
    }
}
