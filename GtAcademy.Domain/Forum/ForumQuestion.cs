using GtAcademy.Domain.Common;
using GtAcademy.Domain.Courses;
using GtAcademy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Domain.Forum
{
    public class ForumQuestion : BaseDomain
    {
        public Guid QuestionId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsPublic { get; set; }

        public bool IsClosed { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid CourseId { get; set; }

        public Guid UserId { get; set; }

        public Course Course { get; set; }

        public User User { get; set; }

        public List<ForumAnswer> ForumAnswers { get; set; }
    }
}
