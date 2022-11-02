using Khoaluan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Khoaluan.Configurations
{
    public class RefundConfiguration:IEntityTypeConfiguration<Refund>
    {
        public void Configure(EntityTypeBuilder<Refund> builder)
        {
            builder.ToTable(nameof(Refund));
            builder.HasKey(o => o.Id);
            builder.HasOne(o => o.User).WithMany(y => y.Refunds).HasForeignKey(t => t.UserID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.Product).WithMany(y => y.Refunds).HasForeignKey(t => t.ProductID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
