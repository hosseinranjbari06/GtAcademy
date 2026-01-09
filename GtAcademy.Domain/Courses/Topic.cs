using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Domain.Courses
{
    public class Topic
    {
        public int TopicId { get; set; }

        public string Title { get; set; } = string.Empty;

        public Guid CourseId { get; set; }

        public Course Course { get; set; } = new Course();

        public List<Episode> Episodes { get; set; } = [];
    }
}
