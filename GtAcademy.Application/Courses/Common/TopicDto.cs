using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Common
{
    public class TopicDto
    {
        public int TopicId { get; set; }

        public string Title { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; }

        public List<EpisodeDto> Episodes { get; set; } = [];
    }
}
