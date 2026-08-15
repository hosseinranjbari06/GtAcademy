using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Queries.GetForumQuestionsList
{
    public class ForumQuestionsListItemDto
    {
        public Guid QuestionId { get; set; }

        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid CourseId { get; set; }

        public int AnswersCount { get; set; }

        public string UserAvatarName { get; set; }

        public string CourseTitle { get; set; }

        public bool IsClosed { get; set; }
    }
}
