using GtAcademy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Domain.Courses
{
    public class Course
    {
        public Guid CourseId { get; set; }

        public string Title { get; set; }

        public string BannerName { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
