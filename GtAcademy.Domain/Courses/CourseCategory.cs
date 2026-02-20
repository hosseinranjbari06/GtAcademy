using GtAcademy.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Domain.Courses
{
    public class CourseCategory : BaseDomain
    {
        public Guid CategoryId { get; set; }

        public string Title { get; set; } = string.Empty;

        public List<Course> Courses { get; set; }
    }
}
