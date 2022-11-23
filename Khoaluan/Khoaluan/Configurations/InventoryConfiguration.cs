using Khoaluan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Khoaluan.Configurations
{
    public class InventoryConfiguration:IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable(nameof(Inventory));
            builder.HasKey(o => new { o.UserID, o.ItemID });
            builder.HasOne(o => o.User).WithMany(y => y.Inventories).HasForeignKey(x => x.UserID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.Item).WithMany(y => y.Inventories).HasForeignKey(x => x.ItemID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
