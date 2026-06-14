using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Episodes.Commands.CreateEpisode
{
    public record CreateEpisodeCommand(CreateEpisodeDto EpisodeDto) : IRequest<ErrorOr<Guid>>;
}
