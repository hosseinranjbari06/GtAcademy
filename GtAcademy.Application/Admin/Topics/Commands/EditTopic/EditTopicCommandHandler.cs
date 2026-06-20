using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Topics.Commands.EditTopic
{
    public class EditTopicCommandHandler : IRequestHandler<EditTopicCommand, ErrorOr<int>>
    {
        private readonly ITopicService _topicService;

        private readonly IGenericService<Topic> _genericTopicService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<EditTopicDto> _validator;

        public EditTopicCommandHandler(ITopicService topicService, IGenericService<Topic> genericTopicService, IUnitOfWork unitOfWork, IValidator<EditTopicDto> validator)
        {
            _topicService = topicService;
            _genericTopicService = genericTopicService;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<ErrorOr<int>> Handle(EditTopicCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.TopicDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            var topic = await _topicService.GetTopicWithRelations(request.TopicDto.TopicId);

            if (topic == null) return Error.NotFound();

            topic.Title = request.TopicDto.Title;

            _genericTopicService.Update(topic);
            await _unitOfWork.CommitAsync();

            return topic.TopicId;
        }
    }
}
