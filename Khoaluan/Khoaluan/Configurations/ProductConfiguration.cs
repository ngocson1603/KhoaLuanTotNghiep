using Khoaluan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Khoaluan.Configurations
{
    public class ProductConfiguration:IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product));
            builder.HasKey(o => o.Id);
            builder.HasOne(o=>o.Developer).WithMany(y=>y.Products).HasForeignKey(x=>x.DevId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(o => o.Items).WithOne(y => y.Product).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
