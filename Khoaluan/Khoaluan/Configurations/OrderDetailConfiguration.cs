using Khoaluan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Khoaluan.Configurations
{
    public class OrderDetailConfiguration:IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable(nameof(OrderDetail));
            builder.HasKey(o => new {o.Id,o.ProductID});
            builder.HasOne(o => o.Product).WithMany(y => y.OrderDetails).HasForeignKey(t => t.ProductID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
