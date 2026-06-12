using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Topics.Commands.EditTopic
{
    public class EditTopicDto
    {
        public int TopicId { get; set; }

        public Guid CourseId { get; set; }

        public string Title { get; set; }
    }
}
