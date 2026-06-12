using GtAcademy.Domain.Courses;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Common.Interfaces
{
    public interface ITopicService
    {
        Task<List<Topic>?> GetCoursesTopics(Guid courseId);

        Task<Topic?> GetTopicForEdit(int topicId);
    }
}
