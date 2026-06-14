using GtAcademy.Domain.Common;

namespace GtAcademy.Domain.Courses
{
    public class Episode : BaseDomain
    {
        public Guid EpisodeId { get; set; }

        public Guid CourseId { get; set; }

        public string Title { get; set; } = string.Empty;

        public TimeSpan Time { get; set; }

        public string FileName { get; set; } = string.Empty;

        public bool IsFree { get; set; }

        public DateTime CreateDate { get; set; }

        public int TopicId { get; set; }

        public Topic Topic { get; set; } = new Topic();
    }
}
