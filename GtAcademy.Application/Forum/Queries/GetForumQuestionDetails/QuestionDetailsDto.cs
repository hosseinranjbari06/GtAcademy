using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Queries.GetForumQuestionDetails
{
    public class QuestionDetailsDto
    {
        public Guid QuestionId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsClosed { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid CourseId { get; set; }

        public string CourseTitle { get; set; }

        public string UserAvatarName { get; set; }

        public string UserName { get; set; }

        public List<AnswerDetailsDto> ForumAnswerDtos { get; set; }
    }
}
