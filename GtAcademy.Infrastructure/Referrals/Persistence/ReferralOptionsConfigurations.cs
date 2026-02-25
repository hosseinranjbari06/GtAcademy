using GtAcademy.Domain.Referral;
using GtAcademy.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Referrals.Persistence
{
    public class ReferralOptionsConfigurations : IEntityTypeConfiguration<ReferralOptions>
    {
        public void Configure(EntityTypeBuilder<ReferralOptions> builder)
        {
            builder.HasKey(o => o.ReferralOptionsId);

            builder.Property(o => o.ReferralOptionsId)
                .ValueGeneratedNever();

            builder.Property(o => o.RewardPercent)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(new ReferralOptions() { ReferralOptionsId = 1, RewardPercent = 1 });
        }
    }
}
