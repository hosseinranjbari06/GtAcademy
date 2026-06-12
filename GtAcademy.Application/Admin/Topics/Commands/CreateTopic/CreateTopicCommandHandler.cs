using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Topics.Commands.CreateTopic
{
    public class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand, ErrorOr<int>>
    {
        private readonly IGenericService<Topic> _genericTopicService;

        private readonly IGenericService<Course> _genericCourseService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<CreateTopicDto> _validator;

        public CreateTopicCommandHandler(IGenericService<Topic> genericTopicService, IUnitOfWork unitOfWork, IValidator<CreateTopicDto> validator, IGenericService<Course> genericCourseService)
        {
            _genericTopicService = genericTopicService;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _genericCourseService = genericCourseService;
        }

        public async Task<ErrorOr<int>> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.TopicDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            var course = await _genericCourseService.GetByIdAsync(request.TopicDto.CourseId);

            if (course == null) return Error.NotFound();

            var topic = new Topic()
            {
                Title = request.TopicDto.Title,
                CourseId = request.TopicDto.CourseId,
                CreateDate = DateTime.Now,
                Course = course
            };

            await _genericTopicService.AddAsync(topic);
            await _unitOfWork.CommitAsync();

            return topic.TopicId;
        }
    }
}
