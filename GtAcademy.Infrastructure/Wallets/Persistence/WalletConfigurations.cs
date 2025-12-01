using GtAcademy.Domain.Wallets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Wallets.Persistence
{
    public class WalletConfigurations : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(w => w.WalletId);

            builder.Property(w => w.WalletId)
                .ValueGeneratedNever();

            builder.Property(c => c.UserId)
                .IsRequired();
        }
    }
}
