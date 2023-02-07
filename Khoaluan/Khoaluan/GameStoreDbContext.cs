using Khoaluan.Configurations;
using Microsoft.EntityFrameworkCore;
using Khoaluan.Models;
using Khoaluan.ModelViews;
using Khoaluan.OtpModels;

namespace Khoaluan
{
    public class GameStoreDbContext:DbContext
    {
        public GameStoreDbContext(DbContextOptions op):base(op)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DeveloperConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new LibraryConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new MarketConfiguration());
            modelBuilder.ApplyConfiguration(new RefundConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new MarketTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new FundConfiguration());
            modelBuilder.ApplyConfiguration(new SaleConfiguration());
            modelBuilder.ApplyConfiguration(new SaleProductConfiguration());
            modelBuilder.ApplyConfiguration(new BlogConfigurationcs());
            modelBuilder.ApplyConfiguration(new AddFundTransactionConfiguration());
            modelBuilder.Entity<MarketTransaction>().Property(e => e.TotalPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Market>().Property(e => e.PricePerItem).HasPrecision(18, 2);
            modelBuilder.Entity<Item>().Property(e=>e.MaxPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Item>().Property(e => e.MinPrice).HasPrecision(18, 2);
            modelBuilder.Entity<OrderDetail>().Property(e => e.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Product>().Property(e => e.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Users>().Property(e => e.Balance).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(e => e.TotalPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Refund>().Property(e => e.Price).HasPrecision(18, 2);
        }
    }
}
