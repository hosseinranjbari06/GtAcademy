using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using GtAcademy.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Episodes.Persistence
{
    public class EpisodeService : IEpisodeService
    {
        private readonly GtAcademyDbContext _context;

        public EpisodeService(GtAcademyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistByFileName(int topicId, string fileName)
        {
            return await _context.Episodes.AnyAsync(episode => episode.TopicId == topicId && episode.FileName == fileName);
        }

        public async Task<Episode?> GetEpisodeWithRelations(Guid episodeId)
        {
            return await _context.Episodes.Include(episode => episode.Topic).FirstOrDefaultAsync(episode => episode.EpisodeId == episodeId);
        }

        public async Task<List<Episode>?> GetTopicsEpisodes(int topicId)
        {
            var topic = await _context.Topics.Include(topic => topic.Episodes).FirstOrDefaultAsync(topic => topic.TopicId == topicId);

            return topic?.Episodes;
        }
    }
}
