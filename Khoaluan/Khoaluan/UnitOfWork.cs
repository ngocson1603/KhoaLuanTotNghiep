using Khoaluan.Interfaces;
using Khoaluan.InterfacesRepository;
using Microsoft.EntityFrameworkCore;

namespace Khoaluan
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }
        public IMongoContext MongoContext { get; }  
        public IProductCategoryRepository ProductCategoryRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }   
        public ICategoryRepository CategoryRepository { get; set; }
        public IDeveloperRepository DeveloperRepository { get; set; }
        public IUsersRepository UserRepository { get; set; }
        public ILibraryRepository LibraryRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public IInventoryRepository InventoryRepository { get; set; }
        public IMarketRepository MarketRepository { get; set; }
        public IDiscussionRepository DiscussionRepository { get; set; } 
        public IRefundRepository RefundRepository { get; set; }
        public IAdminRepository AdminRepository { get; set; }
        public IOrderDetailRepository OrderDetailRepository { get; set; }
        public UnitOfWork(GameStoreDbContext context,
            IProductCategoryRepository productCategoryRepository,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IDeveloperRepository developerRepository,
            IUsersRepository userRepository,
            ILibraryRepository libraryRepository,
            IOrderRepository orderRepository,
            IInventoryRepository inventoryRepository,
            IMarketRepository marketRepository,
            IDiscussionRepository discussionRepository,
            IRefundRepository refundRepository,
            IAdminRepository adminRepository,
            IMongoContext mongoContext,
            IOrderDetailRepository orderDetailRepository
            )
        {
            MongoContext = mongoContext;
            Context = context;
            ProductCategoryRepository = productCategoryRepository;
            ProductRepository= productRepository;
            CategoryRepository= categoryRepository;
            DeveloperRepository= developerRepository;
            UserRepository = userRepository;
            LibraryRepository = libraryRepository;
            OrderRepository = orderRepository;
            InventoryRepository = inventoryRepository;
            MarketRepository = marketRepository;
            DiscussionRepository = discussionRepository;
            RefundRepository = refundRepository;
            AdminRepository = adminRepository;
            OrderDetailRepository = orderDetailRepository;
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
