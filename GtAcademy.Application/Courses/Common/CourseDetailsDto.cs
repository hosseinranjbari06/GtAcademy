using GtAcademy.Application.Users.Common;
using GtAcademy.Domain.Courses;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Common
{
    public class CourseDetailsDto
    {
        public Guid CourseId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string BannerName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Tags { get; set; } = string.Empty;

        public int Price { get; set; }

        public int EpisodeCount { get; set; }

        public TimeSpan TotalTime { get; set; }

        public List<TopicDto> Topics { get; set; } = [];

        public UserSummaryDto TeacherSummary { get; set; } = new UserSummaryDto();

        public DateTime LastUpdateDate { get; set; }

        public List<CourseCommentDto> CourseComments { get; set; } = [];

        public List<CourseCategoryDto> CourseCategories { get; set; } = new List<CourseCategoryDto>();
    }
}
