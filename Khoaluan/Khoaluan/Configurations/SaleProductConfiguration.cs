using Khoaluan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Khoaluan.Configurations
{
    public class SaleProductConfiguration:IEntityTypeConfiguration<SaleProduct>
    {
        public void Configure(EntityTypeBuilder<SaleProduct> builder)
        {
            builder.ToTable(nameof(SaleProduct));
            builder.HasKey(o => o.Id);
            builder.HasOne(o => o.Sale).WithMany(y => y.SaleProducts).HasForeignKey(t => t.SaleID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.Product).WithMany(y => y.SaleProducts).HasForeignKey(t => t.ProductID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
