using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using GtAcademy.Domain.Orders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Orders.Commands.AddCourseToOrder
{
    public class AddCourseToOrderCommandHandler : IRequestHandler<AddCourseToOrderCommand, ErrorOr<bool>>
    {
        private readonly IGenericService<Order> _orderGenericService;

        private readonly IGenericService<Course> _courseGenericService;

        private readonly IOrderService _orderService;

        private readonly IUnitOfWork _unitOfWork;

        public AddCourseToOrderCommandHandler(IGenericService<Order> orderGenericService, IOrderService orderService, IUnitOfWork unitOfWork, IGenericService<Course> courseGenericService)
        {
            _orderGenericService = orderGenericService;
            _orderService = orderService;
            _unitOfWork = unitOfWork;
            _courseGenericService = courseGenericService;
        }

        public async Task<ErrorOr<bool>> Handle(AddCourseToOrderCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseGenericService.GetByIdAsync(request.CourseId);

            if (course == null)
                return Error.NotFound();

            var order = await _orderService.GetUserCurrentOrderIncludeItems(request.UserId);

            if(order == null)
            {
                order = new Order()
                {
                    OrderId = Guid.NewGuid(),
                    UserId = request.UserId,
                    IsPaid = false,
                    CreateDate = DateTime.Now,
                    ItemsCount = 1,
                    TotalAmount = course.Price
                };

                order.Courses.Add(course);
                await _orderGenericService.AddAsync(order);
            }
            else
            {
                order.TotalAmount += course.Price;
                order.ItemsCount += 1;
                order.Courses.Add(course);

                _orderGenericService.Update(order);
            }

            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
