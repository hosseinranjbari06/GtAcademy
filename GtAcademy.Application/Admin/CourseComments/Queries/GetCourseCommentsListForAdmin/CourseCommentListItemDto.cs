using GtAcademy.Application.Users.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseComments.Queries.GetCourseCommentsListForAdmin
{
    public class CourseCommentListItemDto
    {
        public Guid CommentId { get; set; }

        public string Content { get; set; } = string.Empty;

        public Guid CourseId { get; set; }

        public string CourseTitle { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; }

        public bool AdminSubmited { get; set; }

        public UserSummaryDto User { get; set; } = new UserSummaryDto();
    }
}
