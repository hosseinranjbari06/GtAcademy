using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Application.Orders.Common;
using GtAcademy.Domain.Orders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Orders.Queries.GetOrder
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, ErrorOr<OrderDetailsDto>>
    {
        private readonly IOrderService _orderService;

        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<ErrorOr<OrderDetailsDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetOrderByIdIncludeItems(request.OrderId);

            if (order == null)
                return Error.NotFound();

            var orderDto = _mapper.Map<OrderDetailsDto>(order);

            orderDto.Courses = order.Courses
                .Select(course => _mapper.Map<CourseSummaryDto>(course)).ToList();

            return orderDto;
        }
    }
}
