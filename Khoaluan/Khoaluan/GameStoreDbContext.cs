using Khoaluan.Configurations;
using Microsoft.EntityFrameworkCore;
using Khoaluan.Models;
using Khoaluan.ModelViews;

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
        }
        public DbSet<Khoaluan.Models.Product> Product { get; set; }
        public DbSet<Khoaluan.ModelViews.RegisterViewModel> RegisterViewModel { get; set; }
        public DbSet<Khoaluan.Models.Users> User { get; set; }
    }
}
