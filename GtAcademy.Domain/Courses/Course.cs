using GtAcademy.Domain.Common;
using GtAcademy.Domain.Orders;

namespace GtAcademy.Domain.Courses
{
    public class Course : BaseDomain
    {
        public Guid CourseId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string BannerName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Tags { get; set; } = string.Empty;

        public int Price { get; set; }

        public Guid TeacherId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public List<Order> Orders { get; set; } = [];
    }
}
