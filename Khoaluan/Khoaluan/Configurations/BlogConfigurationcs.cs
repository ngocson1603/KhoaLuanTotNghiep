using Khoaluan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Khoaluan.Configurations
{
    public class BlogConfigurationcs:IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable(nameof(Blog));
            builder.HasKey(o => o.Id);
            builder.HasOne(o => o.Admin).WithMany(t => t.Blogs).HasForeignKey(y => y.Ad_Username).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
