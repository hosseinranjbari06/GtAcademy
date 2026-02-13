using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Commands.CreateCourseComment
{
    public class CreateCourseCommentDto
    {
        public Guid CourseId { get; set; }

        public Guid UserId { get; set; }

        public string Content { get; set; } = string.Empty;
    }
}
