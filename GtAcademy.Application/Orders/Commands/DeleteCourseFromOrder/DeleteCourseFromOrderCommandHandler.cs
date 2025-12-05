using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using GtAcademy.Domain.Orders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Orders.Commands.DeleteCourseFromOrder
{
    public class DeleteCourseFromOrderCommandHandler : IRequestHandler<DeleteCourseFromOrderCommand, ErrorOr<bool>>
    {
        private readonly IGenericService<Order> _orderGenericService;

        private readonly IGenericService<Course> _courseGenericService;

        private readonly IOrderService _orderService;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourseFromOrderCommandHandler(IGenericService<Order> orderGenericService, IOrderService orderService, IUnitOfWork unitOfWork, IGenericService<Course> courseGenericService)
        {
            _orderGenericService = orderGenericService;
            _orderService = orderService;
            _unitOfWork = unitOfWork;
            _courseGenericService = courseGenericService;
        }
        public async Task<ErrorOr<bool>> Handle(DeleteCourseFromOrderCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseGenericService.GetByIdAsync(request.CourseId);

            if (course == null)
                return Error.NotFound();

            var order = await _orderService.GetUserCurrentOrderIncludeItems(request.UserId);

            if (order == null)
                return Error.NotFound();

            if (order.Courses.Contains(course))
            {
                order.Courses.Remove(course);
                order.TotalAmount -= course.Price;
                order.ItemsCount -= 1;

                _orderGenericService.Update(order);
                await _unitOfWork.CommitAsync();

                return true;
            }

            return false;
        }
    }
}
