using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Episodes.Commands.EditEpisode
{
    public class EditEpisodeDto
    {
        public Guid EpisodeId { get; set; }

        public string Title { get; set; } = string.Empty;

        public TimeSpan Time { get; set; }

        public string FileName { get; set; } = string.Empty;

        public bool IsFree { get; set; }

        public int TopicId { get; set; }
    }
}
