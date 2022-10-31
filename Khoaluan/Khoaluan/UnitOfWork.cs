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
        public IUsersRepository UserRepository { get; set; }
        public ILibraryRepository LibraryRepository { get; set; }
        public UnitOfWork(GameStoreDbContext context,
            IProductCategoryRepository productCategoryRepository,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IDeveloperRepository developerRepository,
            IUsersRepository userRepository,
            ILibraryRepository libraryRepository
            )
        {
            Context = context;
            ProductCategoryRepository = productCategoryRepository;
            ProductRepository= productRepository;
            CategoryRepository= categoryRepository;
            DeveloperRepository= developerRepository;
            UserRepository = userRepository;
            LibraryRepository = libraryRepository;
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
