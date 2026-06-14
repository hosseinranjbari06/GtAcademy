using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using MediatR;

namespace GtAcademy.Application.Admin.Episodes.Commands.CreateEpisode
{
    public class CreateEpisodeCommandHandler : IRequestHandler<CreateEpisodeCommand, ErrorOr<Guid>>
    {
        private readonly IValidator<CreateEpisodeDto> _validator;

        private readonly IEpisodeService _episodeService;

        private readonly ICourseService _courseService;

        private readonly IGenericService<Episode> _genericEpisodeService;

        private readonly IGenericService<Topic> _genericTopicService;

        private readonly IGenericService<Course> _genericCourseService;

        private readonly IUnitOfWork _unitOfWork;

        public CreateEpisodeCommandHandler(IValidator<CreateEpisodeDto> validator, IGenericService<Episode> genericEpisodeService, IGenericService<Topic> genericTopicService, IUnitOfWork unitOfWork, IEpisodeService episodeService, IGenericService<Course> genericCourseService, ICourseService courseService)
        {
            _validator = validator;
            _genericEpisodeService = genericEpisodeService;
            _genericTopicService = genericTopicService;
            _unitOfWork = unitOfWork;
            _episodeService = episodeService;
            _genericCourseService = genericCourseService;
            _courseService = courseService;
        }

        public async Task<ErrorOr<Guid>> Handle(CreateEpisodeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.EpisodeDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            var topic = await _genericTopicService.GetByIdAsync(request.EpisodeDto.TopicId);

            if (topic == null) return Error.NotFound();

            if (await _episodeService.ExistByFileName(topic.TopicId, request.EpisodeDto.FileName))
            {
                return Error.Validation("FileName", "نام فایل انتخاب شده تکراری میباشد");
            }

            var episode = new Episode()
            {
                EpisodeId = Guid.NewGuid(),
                TopicId = topic.TopicId,
                CourseId = topic.CourseId,
                Title = request.EpisodeDto.Title,
                Time = request.EpisodeDto.Time,
                FileName = request.EpisodeDto.FileName,
                IsFree = request.EpisodeDto.IsFree,
                CreateDate = DateTime.Now,
                Topic = topic
            };

            var course = await _courseService.GetCourseForEditById(topic.CourseId);

            if (course == null) return Error.NotFound();

            course.EpisodeCount += 1;
            course.TotalTime += episode.Time;

            await _genericEpisodeService.AddAsync(episode);
            _genericCourseService.Update(course);
            await _unitOfWork.CommitAsync();

            return topic.CourseId;
        }
    }
}
