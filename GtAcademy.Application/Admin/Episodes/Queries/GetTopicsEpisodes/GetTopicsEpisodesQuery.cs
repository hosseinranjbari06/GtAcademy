using ErrorOr;
using GtAcademy.Application.Courses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Episodes.Queries.GetTopicsEpisodes
{
    public record GetTopicsEpisodesQuery(int TopicId) : IRequest<ErrorOr<List<EpisodeDto>>>;
}
