using Khoaluan.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Khoaluan.Configurations
{
    public class MarketTransactionConfiguration:IEntityTypeConfiguration<MarketTransaction>
    {
        public void Configure(EntityTypeBuilder<MarketTransaction> builder)
        {
            builder.ToTable(nameof(MarketTransaction));
            builder.HasKey(o => o.Id);
            builder.HasOne(o=>o.Market).WithMany(y=>y.MarketTransactions).HasForeignKey(t=>t.MarketID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o=>o.Buyer).WithMany(y=>y.Buys).HasForeignKey(t=>t.BuyerID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.Seller).WithMany(y => y.Sells).HasForeignKey(t => t.SellerID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.Item).WithMany(y => y.MarketTransactions).HasForeignKey(t => t.ItemID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
