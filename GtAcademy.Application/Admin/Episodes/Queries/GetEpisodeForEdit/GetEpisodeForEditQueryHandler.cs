using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Admin.Episodes.Commands.EditEpisode;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Episodes.Queries.GetEpisodeForEdit
{
    public class GetEpisodeForEditQueryHandler : IRequestHandler<GetEpisodeForEditQuery, ErrorOr<EditEpisodeDto>>
    {
        private readonly IGenericService<Episode> _genericEpisodeService;

        private readonly IMapper _mapper;

        public GetEpisodeForEditQueryHandler(IGenericService<Episode> genericEpisodeService, IMapper mapper)
        {
            _genericEpisodeService = genericEpisodeService;
            _mapper = mapper;
        }

        public async Task<ErrorOr<EditEpisodeDto>> Handle(GetEpisodeForEditQuery request, CancellationToken cancellationToken)
        {
            var episode = await _genericEpisodeService.GetByIdAsync(request.EpisodeId);

            if (episode == null) return Error.NotFound();

            return _mapper.Map<EditEpisodeDto>(episode);
        }
    }
}
