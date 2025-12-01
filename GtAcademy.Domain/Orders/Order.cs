using GtAcademy.Domain.Courses;
using GtAcademy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Domain.Orders
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }

        public int TotalAmount { get; set; }

        public int ItemsCount { get; set; }

        public bool IsPaid { get; set; }

        public DateTime? PaymentDate { get; set; }

        public DateTime CreateDate { get; set; }

        public User User { get; set; }

        public List<Course> Courses { get; set; }
    }
}
