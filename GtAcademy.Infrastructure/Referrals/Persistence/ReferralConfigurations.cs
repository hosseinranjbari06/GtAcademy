using GtAcademy.Domain.Referral;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GtAcademy.Infrastructure.Referrals.Persistence
{
    public class ReferralConfigurations : IEntityTypeConfiguration<Referral>
    {
        public void Configure(EntityTypeBuilder<Referral> builder)
        {
            builder.HasKey(r => r.ReferralId);

            builder
                .HasOne(r => r.Referrer)
                .WithMany(u => u.ReferralsSent)
                .HasForeignKey(r => r.ReferrerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(r => r.Referred)
                .WithOne(u => u.ReferralReceived)
                .HasForeignKey<Referral>(r => r.ReferredId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasIndex(r => r.ReferredId)
                .IsUnique();
    }
    }
}
