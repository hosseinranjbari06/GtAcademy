using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Commands.AnswerQuestion
{
    public class CreateAnswerDto
    {
        public string Content { get; set; }

        public Guid QuestionId { get; set; }

        public Guid UserId { get; set; }
    }
}
