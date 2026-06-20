using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Topics.Commands.DeleteTopic
{
    public class DeleteTopicCommandHandler : IRequestHandler<DeleteTopicCommand, ErrorOr<Guid>>
    {
        private readonly ITopicService _topicService;

        private readonly IGenericService<Topic> _genericTopicService;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteTopicCommandHandler(ITopicService topicService, IGenericService<Topic> genericTopicService, IUnitOfWork unitOfWork)
        {
            _topicService = topicService;
            _genericTopicService = genericTopicService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Guid>> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = await _topicService.GetTopicWithRelations(request.TopicId);

            if (topic == null) return Error.NotFound();

            if (topic.Episodes.Any()) return Error.Validation("All", "این سرفصل دارای اپیزود است. برای حذف ان ابتدا اپیزود هایش را حذف کنید.");

            _genericTopicService.Delete(topic);
            await _unitOfWork.CommitAsync();

            return topic.CourseId;
        }
    }
}
