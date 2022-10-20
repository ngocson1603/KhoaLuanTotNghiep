using Khoaluan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Khoaluan.Configurations
{
    public class ProductCategoryConfiguration:IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable(nameof(ProductCategory));
            builder.HasKey(o => new {o.ProductId,o.CategoryId});
            builder.HasOne(o=>o.Product).WithMany(y=>y.ProductCategories).HasForeignKey(x=>x.ProductId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.Category).WithMany(y => y.ProductCategories).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
