using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Queries.GetForumQuestionDetails
{
    public class AnswerDetailsDto
    {
        public Guid AnswerId { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsAcceptedAnswer { get; set; }

        public string UserAvatarName { get; set; }

        public string UserName { get; set; }
    }
}
