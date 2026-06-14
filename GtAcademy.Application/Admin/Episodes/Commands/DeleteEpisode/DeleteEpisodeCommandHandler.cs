using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Episodes.Commands.DeleteEpisode
{
    public class DeleteEpisodeCommandHandler : IRequestHandler<DeleteEpisodeCommand, ErrorOr<(int, Guid, string)>>
    {
        private readonly IGenericService<Episode> _genericEpisodeService;

        private readonly IGenericService<Course> _genericCourseService;

        private readonly IEpisodeService _episodeService;

        private readonly ICourseService _courseService;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteEpisodeCommandHandler(IGenericService<Episode> genericEpisodeService, IGenericService<Course> genericCourseService, ICourseService courseService, IUnitOfWork unitOfWork, IEpisodeService episodeService)
        {
            _genericEpisodeService = genericEpisodeService;
            _genericCourseService = genericCourseService;
            _courseService = courseService;
            _unitOfWork = unitOfWork;
            _episodeService = episodeService;
        }

        public async Task<ErrorOr<(int, Guid, string)>> Handle(DeleteEpisodeCommand request, CancellationToken cancellationToken)
        {
            var episode = await _episodeService.GetEpisodeWithRelations(request.EpisodeId);

            if (episode == null) return Error.NotFound();

            var course = await _courseService.GetCourseForEditById(episode.Topic.CourseId);

            if (course == null) return Error.NotFound();

            course.EpisodeCount -= 1;
            course.TotalTime -= episode.Time;

            _genericEpisodeService.Delete(episode);
            _genericCourseService.Update(course);

            await _unitOfWork.CommitAsync();

            return (episode.TopicId, episode.Topic.CourseId, episode.FileName);
        }
    }
}
