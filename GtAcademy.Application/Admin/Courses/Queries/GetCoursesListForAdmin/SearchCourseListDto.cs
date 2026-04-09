using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Courses.Queries.GetCoursesListForAdmin
{
    public class SearchCourseListDto
    {
        public string Title { get; set; } = string.Empty;

        public string Tags { get; set; } = string.Empty;

        public string? OrderBy { get; set; }

        public int PageId { get; set; } = 1;

        public int Take { get; set; } = 50;
    }
}
