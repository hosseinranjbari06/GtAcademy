using GtAcademy.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Orders.Persistence
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderId);

            builder.Property(o => o.OrderId)
                .ValueGeneratedNever();

            builder.Property(o => o.UserId)
                .IsRequired();
        }
    }
}
