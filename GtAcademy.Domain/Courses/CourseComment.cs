using GtAcademy.Domain.Common;
using GtAcademy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Domain.Courses
{
    public class CourseComment : BaseDomain
    {
        public Guid CommentId { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; }

        public bool AdminSubmited { get; set; }

        public Guid UserId { get; set; }

        public Guid CourseId { get; set; }

        public Course Course { get; set; } = new Course();
    }
}
