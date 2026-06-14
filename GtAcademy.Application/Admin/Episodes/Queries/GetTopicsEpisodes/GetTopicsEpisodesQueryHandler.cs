using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Courses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Episodes.Queries.GetTopicsEpisodes
{
    public class GetTopicsEpisodesQueryHandler : IRequestHandler<GetTopicsEpisodesQuery, ErrorOr<List<EpisodeDto>>>
    {
        private readonly IEpisodeService _episodeService;

        private readonly IMapper _mapper;

        public GetTopicsEpisodesQueryHandler(IEpisodeService episodeService, IMapper mapper)
        {
            _episodeService = episodeService;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<EpisodeDto>>> Handle(GetTopicsEpisodesQuery request, CancellationToken cancellationToken)
        {
            var episodes = await _episodeService.GetTopicsEpisodes(request.TopicId);

            if (episodes == null) return Error.NotFound();

            return episodes.Select(_mapper.Map<EpisodeDto>).OrderByDescending(episode => episode.CreateDate).ToList();
        }
    }
}
