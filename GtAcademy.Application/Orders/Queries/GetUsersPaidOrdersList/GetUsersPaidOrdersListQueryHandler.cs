using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Orders.Queries.GetUsersPaidOrdersList
{
    public class GetUsersPaidOrdersListQueryHandler : IRequestHandler<GetUsersPaidOrdersListQuery, ErrorOr<List<OrderListItemDto>>>
    {
        private readonly IOrderService _orderService;

        private readonly IMapper _mapper;

        public GetUsersPaidOrdersListQueryHandler(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<OrderListItemDto>>> Handle(GetUsersPaidOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetUsersPaidOrdersList(request.UserId);

            if (orders == null) return new List<OrderListItemDto>();

            return orders.Select(_mapper.Map<OrderListItemDto>).OrderByDescending(order => order.PaymentDate).ToList();
        }
    }
}
