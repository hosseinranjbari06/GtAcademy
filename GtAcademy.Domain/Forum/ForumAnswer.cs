using GtAcademy.Domain.Common;
using GtAcademy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Domain.Forum
{
    public class ForumAnswer : BaseDomain
    {
        public Guid AnswerId { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsAcceptedAnswer { get; set; }

        public Guid QuestionId { get; set; }

        public Guid UserId { get; set; }

        public ForumQuestion ForumQuestion { get; set; }

        public User User { get; set; }
    }
}
