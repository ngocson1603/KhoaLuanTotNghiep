using Khoaluan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Khoaluan.Configurations
{
    public class OrderConfiguration:IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(nameof(Order));
            builder.HasKey(o => o.Id);
            builder.HasOne(o => o.User).WithMany(y => y.Orders).HasForeignKey(x => x.UserID).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(o => o.OrderDetails).WithOne(y => y.Order).HasForeignKey(x => x.Id).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
