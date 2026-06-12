using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Topics.Commands.CreateTopic
{
    public class CreateTopicDto
    {
        public Guid CourseId { get; set; }

        public string Title { get; set; }
    }
}
