using GtAcademy.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Common.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> GetOrderByIdIncludeItems(Guid orderId);

        Task<Order?> GetUserCurrentOrderIncludeItems(Guid userId);
    }
}
