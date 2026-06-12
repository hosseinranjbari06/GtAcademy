using GtAcademy.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Domain.Courses
{
    public class Topic : BaseDomain
    {
        public int TopicId { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; }

        public Guid CourseId { get; set; }

        public Course Course { get; set; } = new Course();

        public List<Episode> Episodes { get; set; } = [];
    }
}
