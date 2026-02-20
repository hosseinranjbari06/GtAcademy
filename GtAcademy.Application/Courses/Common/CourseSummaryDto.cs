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

        public string Title { get; set; } = string.Empty;

        public string BannerName { get; set; } = string.Empty;

        public int Price { get; set; }

        public int EpisodeCount { get; set; }

        public TimeSpan TotalTime { get; set; }

        public UserSummaryDto TeacherSummary { get; set; } = new UserSummaryDto();

        public List<CourseCategoryDto> CourseCategories { get; set; } = new List<CourseCategoryDto>();
    }
}
