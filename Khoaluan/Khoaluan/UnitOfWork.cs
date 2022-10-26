using Khoaluan.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Khoaluan
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }
        public IProductCategoryRepository ProductCategoryRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }   
        public ICategoryRepository CategoryRepository { get; set; }
        public IDeveloperRepository DeveloperRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public UnitOfWork(GameStoreDbContext context,
            IProductCategoryRepository productCategoryRepository,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IDeveloperRepository developerRepository,
            IUserRepository userRepository
            )
        {
            Context = context;
            ProductCategoryRepository = productCategoryRepository;
            ProductRepository= productRepository;
            CategoryRepository= categoryRepository;
            DeveloperRepository= developerRepository;
            UserRepository = userRepository;
        }
        public void Dispose()
        {
            Context?.Dispose();
        }
        public void SaveChange()
        {
            Context.SaveChanges();
        }
    }
}
