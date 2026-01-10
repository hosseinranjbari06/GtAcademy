using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Queries.HasUserPermissionToEpisode
{
    public class HasUserPermissionToEpisodeQueryHandler : IRequestHandler<HasUserPermissionToEpisodeQuery, ErrorOr<string>>
    {
        private readonly IGenericService<Episode> _genericEpisodeService;
        private readonly IOrderService _orderService;

        public HasUserPermissionToEpisodeQueryHandler(IGenericService<Episode> genericEpisodeService, IOrderService orderService)
        {
            _genericEpisodeService = genericEpisodeService;
            _orderService = orderService;
        }

        public async Task<ErrorOr<string>> Handle(HasUserPermissionToEpisodeQuery request, CancellationToken cancellationToken)
        {
            var episode = await _genericEpisodeService.GetByIdAsync(request.EpisodeId);

            if (episode == null) return Error.NotFound();

            if (episode.IsFree) return episode.FileName;

            var result = await _orderService.HasUserBoughtCourse(request.UserId, episode.CourseId);

            if (result == false) return Error.Unauthorized(description:"کاربر به فایل مورد نظر دسترسی ندارد");

            return episode.FileName;
        }
    }
}
