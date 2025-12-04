using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Common
{
    public class CreateCourseDto
    {
        public string Title { get; set; } = string.Empty;

        public string BannerName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Tags { get; set; } = string.Empty;

        public int Price { get; set; }

        public Guid CreatorId { get; set; }
    }
}
