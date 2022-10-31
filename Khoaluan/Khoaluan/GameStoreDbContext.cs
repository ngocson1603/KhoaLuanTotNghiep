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
        }
        public DbSet<Khoaluan.OtpModels.LibraryModelDetail> LibraryModelDetail { get; set; }
    }
}
