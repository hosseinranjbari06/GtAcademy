using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Queries.GetForumQuestionsList
{
    public class ForumQuestionsSearchDto
    {
        public int PageId { get; set; } = 1;

        public int Take { get; set; } = 15;

        public string TitleSearch { get; set; } = string.Empty;

        public Guid? CourseId { get; set; }

        public bool? IsClosed { get; set; }
    }
}
