using ErrorOr;
using GtAcademy.Application.Admin.CourseComments.Commands.DeleteCourseCommentByAdmin;
using GtAcademy.Application.Admin.Episodes.Commands.DeleteEpisode;
using GtAcademy.Application.Admin.Topics.Commands.DeleteTopic;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using GtAcademy.Domain.Orders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Courses.Commands.DeleteCourseByAdmin
{
    public class DeleteCourseByAdminCommandHandler : IRequestHandler<DeleteCourseByAdminCommand, ErrorOr<bool>>
    {
        private readonly ICourseService _courseService;

        private readonly IGenericService<Course> _genericCourseService;

        private readonly IGenericService<Order> _genericOrderService;

        private readonly IMediator _mediator;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourseByAdminCommandHandler(ICourseService courseService, IGenericService<Course> genericCourseService, IMediator mediator, IUnitOfWork unitOfWork, IGenericService<Order> genericOrderService)
        {
            _courseService = courseService;
            _genericCourseService = genericCourseService;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _genericOrderService = genericOrderService;
        }

        public async Task<ErrorOr<bool>> Handle(DeleteCourseByAdminCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseService.GetCourseWithRelations(request.CourseId);

            if (course == null) return Error.NotFound();

            var iterateTopics = new Topic[course.Topics.Count];
            course.Topics.CopyTo(iterateTopics);

            foreach (var topic in iterateTopics)
            {
                var iterateEpisodes = new Episode[topic.Episodes.Count];
                topic.Episodes.CopyTo(iterateEpisodes);

                foreach (var episode in iterateEpisodes)
                {
                    await _mediator.Send(new DeleteEpisodeCommand(episode.EpisodeId));
                }

                await _mediator.Send(new DeleteTopicCommand(topic.TopicId));
            }

            var iterateComments = new CourseComment[course.CourseComments.Count];
            course.CourseComments.CopyTo(iterateComments);

            foreach (var comment in iterateComments)
            {
                await _mediator.Send(new DeleteCourseCommentByAdminCommand(comment.CommentId));
            }

            var iterateOrders = new Order[course.Orders.Count];
            course.Orders.Where(order => !order.IsPaid).ToList().CopyTo(iterateOrders);

            foreach (var order in iterateOrders.Where(o => o != null))
            {
                order.TotalAmount -= course.Price;
                order.ItemsCount -= 1;
                order.Courses.Remove(course);

                _genericOrderService.Update(order);
            }

            course.CourseCategories.Clear();
            course.IsDeleted = true;

            _genericCourseService.Update(course);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
