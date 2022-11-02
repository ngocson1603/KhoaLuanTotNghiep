using Khoaluan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Khoaluan.Configurations
{
    public class MarketConfiguration:IEntityTypeConfiguration<Market>
    {
        public void Configure(EntityTypeBuilder<Market> builder)
        {
            builder.ToTable(nameof(Market));
            builder.HasKey(o => o.Id);
            builder.HasOne(o => o.User).WithMany(y => y.Markets).HasForeignKey(t => t.UserID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.Item).WithMany(y => y.Markets).HasForeignKey(t => t.ItemID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
