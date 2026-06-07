using GtAcademy.Domain.Wallets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Wallets.Persistence
{
    public class WalletIncomeConfigurations : IEntityTypeConfiguration<WalletIncome>
    {
        public void Configure(EntityTypeBuilder<WalletIncome> builder)
        {
            builder.HasKey(w => w.WalletIncomeId);

            builder.Property(w => w.WalletIncomeId)
                .ValueGeneratedNever();

            builder.Property(w => w.WalletId)
                .IsRequired();

            builder.Property(w => w.Description)
                .IsRequired()
                .HasMaxLength(300);
        }
    }
}
