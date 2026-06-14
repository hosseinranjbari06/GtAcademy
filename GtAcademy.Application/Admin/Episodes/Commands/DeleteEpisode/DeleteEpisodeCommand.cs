using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Episodes.Commands.DeleteEpisode
{
    public record DeleteEpisodeCommand(Guid EpisodeId) : IRequest<ErrorOr<(int,Guid,string)>>;
}
