using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseComments.Queries.GetCourseCommentsListForAdmin
{
    public class SearchCourseCommentsListDto
    {
        public Guid? CourseId { get; set; }

        public bool AdminSubmited { get; set; } = false;

        public int PageId { get; set; } = 1;

        public int Take { get; set; } = 50;

        public int PagesCount { get; set; }

        public List<CourseCommentListItemDto> Comments { get; set; } = [];
    }
}
