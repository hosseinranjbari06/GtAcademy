using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Orders.Queries.GetUsersPaidOrdersList
{
    public class OrderListItemDto
    {
        public Guid OrderId { get; set; }

        public int TotalAmount { get; set; }

        public int ItemsCount { get; set; }

        public DateTime? PaymentDate { get; set; }
    }
}
