using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Episodes.Commands.CreateEpisode
{
    public class CreateEpisodeDto
    {
        public string Title { get; set; } = string.Empty;

        public TimeSpan Time { get; set; }

        public string FileName { get; set; } = string.Empty;

        public bool IsFree { get; set; }

        public int TopicId { get; set; }
    }
}
