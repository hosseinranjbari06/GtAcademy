using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Episodes.Commands.EditEpisode
{
    public class EditEpisodeCommandHandler : IRequestHandler<EditEpisodeCommand, ErrorOr<Guid>>
    {
        private readonly IValidator<EditEpisodeDto> _validator;

        private readonly IEpisodeService _episodeService;

        private readonly ICourseService _courseService;

        private readonly IGenericService<Episode> _genericEpisodeService;

        private readonly IGenericService<Course> _genericCourseService;

        private readonly IUnitOfWork _unitOfWork;

        public EditEpisodeCommandHandler(IValidator<EditEpisodeDto> validator, IEpisodeService episodeService, ICourseService courseService, IGenericService<Episode> genericEpisodeService, IGenericService<Course> genericCourseService, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _episodeService = episodeService;
            _courseService = courseService;
            _genericEpisodeService = genericEpisodeService;
            _genericCourseService = genericCourseService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Guid>> Handle(EditEpisodeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.EpisodeDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            var episode = await _episodeService.GetEpisodeWithRelations(request.EpisodeDto.EpisodeId);

            if (episode == null) return Error.NotFound();

            if (!string.IsNullOrEmpty(request.EpisodeDto.FileName) && episode.FileName != request.EpisodeDto.FileName)
            {
                if (await _episodeService.ExistByFileName(episode.TopicId, request.EpisodeDto.FileName))
                {
                    return Error.Validation("FileName", "نام فایل انتخاب شده تکراری میباشد");
                }

                episode.FileName = request.EpisodeDto.FileName;
            }

            if (episode.Time != request.EpisodeDto.Time)
            {
                var course = await _courseService.GetCourseForEditById(episode.Topic.CourseId);

                if (course == null) return Error.NotFound();

                course.TotalTime -= episode.Time;
                course.TotalTime += request.EpisodeDto.Time;

                _genericCourseService.Update(course);

                episode.Time = request.EpisodeDto.Time;
            }

            episode.Title = request.EpisodeDto.Title;
            episode.IsFree = request.EpisodeDto.IsFree;

            _genericEpisodeService.Update(episode);
            await _unitOfWork.CommitAsync();

            return episode.Topic.CourseId;
        }
    }
}
