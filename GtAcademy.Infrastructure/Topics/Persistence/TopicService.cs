using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using GtAcademy.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Topics.Persistence
{
    public class TopicService : ITopicService
    {
        private readonly GtAcademyDbContext _context;

        public TopicService(GtAcademyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Topic>?> GetCoursesTopics(Guid courseId)
        {
            var course = await _context.Courses.Include(course => course.Topics).FirstOrDefaultAsync(course => course.CourseId == courseId);
            return course?.Topics;
        }

        public async Task<Topic?> GetTopicWithRelations(int topicId)
        {
            return await _context.Topics
            .Include(topic => topic.Course)
            .Include(topic => topic.Episodes)
            .FirstOrDefaultAsync(topic => topic.TopicId == topicId);
        }
    }
}
