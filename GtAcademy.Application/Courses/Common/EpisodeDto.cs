using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Common
{
    public class EpisodeDto
    {
        public Guid EpisodeId { get; set; }

        public string Title { get; set; } = string.Empty;

        public TimeSpan Time { get; set; }

        public bool IsFree { get; set; }
    }
}
