using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Common
{
    public class CourseCategoryDto
    {
        public Guid CategoryId { get; set; }

        public string Title { get; set; } = string.Empty;
    }
}
