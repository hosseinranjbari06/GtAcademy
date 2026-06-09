using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Queries.HasUserBoughtTheCourse
{
    public class HasUserBoughtTheCourseQueryHandler : IRequestHandler<HasUserBoughtTheCourseQuery, ErrorOr<bool>>
    {
        private readonly IOrderService _orderService;

        public HasUserBoughtTheCourseQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<ErrorOr<bool>> Handle(HasUserBoughtTheCourseQuery request, CancellationToken cancellationToken)
        {
            return await _orderService.HasUserBoughtCourse(request.UserId, request.CourseId);
        }
    }
}
