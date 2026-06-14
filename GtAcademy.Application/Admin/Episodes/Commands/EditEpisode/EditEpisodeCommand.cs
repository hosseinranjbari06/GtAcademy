using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Episodes.Commands.EditEpisode
{
    public record EditEpisodeCommand(EditEpisodeDto EpisodeDto) : IRequest<ErrorOr<Guid>>;
}
