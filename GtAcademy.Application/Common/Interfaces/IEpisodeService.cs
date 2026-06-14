using GtAcademy.Domain.Courses;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Common.Interfaces
{
    public interface IEpisodeService
    {
        Task<List<Episode>?> GetTopicsEpisodes(int topicId);

        Task<bool> ExistByFileName(int topicId, string fileName);

        Task<Episode?> GetEpisodeWithRelations(Guid episodeId);
    }
}
