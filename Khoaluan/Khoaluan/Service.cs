using Khoaluan.InterfacesService;

namespace Khoaluan
{
    public class Service : IService
    {
        public IDiscussionService DiscussionService { get; }
        public IInventoryService InventoryService { get; }
        public ILibraryService LibraryService { get; }
        public IMarketService MarketService { get; }
        public IOrderService OrderService { get; }
        public IProductService ProductService { get; }
        public IRefundService RefundService { get; }
        public IUserService UserService { get; }
        public IMarketTransactionService MarketTransactionService { get; }
        public Service(IDiscussionService discussionService, 
            IInventoryService inventoryService, 
            ILibraryService libraryService, 
            IMarketService marketService, 
            IOrderService orderService, 
            IProductService productService, 
            IRefundService refundService, 
            IUserService userService,
            IMarketTransactionService marketTransactionService
            )
        {
            DiscussionService = discussionService;
            InventoryService = inventoryService;
            LibraryService = libraryService;
            MarketService = marketService;
            OrderService = orderService;
            ProductService = productService;
            RefundService = refundService;
            UserService = userService;
            MarketTransactionService = marketTransactionService;
        }
    }
}
