using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Common
{
    public class TopicDto
    {
        public string Title { get; set; } = string.Empty;

        public List<EpisodeDto> Episodes { get; set; } = [];
    }
}
