using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Commands.CreateQuestion
{
    public class CreateQuestionDto
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsTicket { get; set; }

        public Guid CourseId { get; set; }

        public Guid UserId { get; set; }
    }
}
