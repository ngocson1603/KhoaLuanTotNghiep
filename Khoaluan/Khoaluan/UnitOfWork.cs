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
        public IFundRepository FundRepository { get; set; }
        public IItemRepository ItemRepository { get; set; }
        public IMarketTransactionRepository MarketTransactionRepository { get; set; }
        public ISaleRepository SaleRepository { get; set; }
        public ISaleProductRepository SaleProductRepository { get; set; }
        public IBlogRepository BlogRepository { get; set; }
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
            IOrderDetailRepository orderDetailRepository,
            IFundRepository fundRepository,
            IItemRepository itemRepository,
            IMarketTransactionRepository marketTransactionRepository,
            ISaleRepository saleRepository,
            ISaleProductRepository saleProductRepository,
            IBlogRepository blogRepository
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
            FundRepository = fundRepository;
            ItemRepository = itemRepository;
            MarketTransactionRepository = marketTransactionRepository;
            SaleRepository = saleRepository;
            SaleProductRepository = saleProductRepository;
            BlogRepository = blogRepository;
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
