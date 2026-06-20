using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Queries.HasUserPermissionToEpisode
{
    public class HasUserPermissionToEpisodeQueryHandler : IRequestHandler<HasUserPermissionToEpisodeQuery, ErrorOr<(Guid, int, string)>>
    {
        private readonly IEpisodeService _episodeService;

        private readonly IOrderService _orderService;

        public HasUserPermissionToEpisodeQueryHandler(IOrderService orderService, IEpisodeService episodeService)
        {
            _orderService = orderService;
            _episodeService = episodeService;
        }

        public async Task<ErrorOr<(Guid, int, string)>> Handle(HasUserPermissionToEpisodeQuery request, CancellationToken cancellationToken)
        {
            var episode = await _episodeService.GetEpisodeWithRelations(request.EpisodeId);

            if (episode == null) return Error.NotFound();

            if (episode.IsFree) return (episode.Topic.CourseId, episode.TopicId, episode.FileName);

            var result = await _orderService.HasUserBoughtCourse(request.UserId, episode.CourseId);

            if (result == false) return Error.Unauthorized(description:"کاربر به فایل مورد نظر دسترسی ندارد");

            return (episode.Topic.CourseId, episode.TopicId, episode.FileName);
        }
    }
}
