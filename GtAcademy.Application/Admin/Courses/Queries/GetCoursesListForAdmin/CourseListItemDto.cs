using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Courses.Queries.GetCoursesListForAdmin
{
    public class CourseListItemDto
    {
        public Guid CourseId { get; set; }

        public string Title { get; set; } = string.Empty;

        public int Price { get; set; }

        public int EpisodeCount { get; set; }

        public TimeSpan TotalTime { get; set; }

        public DateTime LastUpdateDate { get; set; }
    }
}
