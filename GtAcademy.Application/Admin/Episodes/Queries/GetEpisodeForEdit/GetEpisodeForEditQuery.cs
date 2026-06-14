using ErrorOr;
using GtAcademy.Application.Admin.Episodes.Commands.EditEpisode;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Episodes.Queries.GetEpisodeForEdit
{
    public record GetEpisodeForEditQuery(Guid EpisodeId) : IRequest<ErrorOr<EditEpisodeDto>>;
}
