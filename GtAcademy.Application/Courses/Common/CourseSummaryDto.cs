using GtAcademy.Application.Users.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Common
{
    public class CourseSummaryDto
    {
        public Guid CourseId { get; set; }

        public Guid TeacherId { get; set; }

        public string Title { get; set; }

        public string BannerName { get; set; }

        public int Price { get; set; }

        public UserSummaryDto TeacherSummary { get; set; }

        public int EpisodesCount { get; set; }
    }
}
