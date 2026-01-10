using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Queries.HasUserPermissionToEpisode
{
    public record HasUserPermissionToEpisodeQuery(Guid UserId, Guid EpisodeId) : IRequest<ErrorOr<string>>;
}
