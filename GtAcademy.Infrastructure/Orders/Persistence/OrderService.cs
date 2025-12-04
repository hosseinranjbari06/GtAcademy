using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Orders;
using GtAcademy.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Orders.Persistence
{
    public class OrderService : IOrderService
    {
        private readonly GtAcademyDbContext _context;

        public OrderService(GtAcademyDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetOrderByIdIncludeItems(Guid orderId)
        {
            return await _context.Orders
                .Where(order => order.OrderId == orderId)
                .Include(order => order.Courses)
                .FirstAsync();
        }

        public async Task<Order?> GetUserCurrentOrderIncludeItems(Guid userId)
        {
            return await _context.Orders
                .Where(order => order.UserId == userId && !order.IsPaid)
                .Include(order => order.Courses)
                .FirstAsync();
        }
    }
}
