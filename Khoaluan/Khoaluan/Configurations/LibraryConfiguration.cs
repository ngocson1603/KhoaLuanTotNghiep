using Khoaluan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net.Mail;

namespace Khoaluan.Configurations
{
    public class LibraryConfiguration:IEntityTypeConfiguration<Library>
    {
        public void Configure(EntityTypeBuilder<Library> builder)
        {
            builder.ToTable(nameof(Library));
            builder.HasKey(o => new { o.UserId, o.ProductId });
            builder.HasOne(o => o.Product).WithMany(y => y.Libraries).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.Users).WithMany(y => y.Libraries).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
