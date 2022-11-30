using Khoaluan.InterfacesService;

namespace Khoaluan
{
    public interface IService
    {
        IDiscussionService DiscussionService { get; }
        IInventoryService InventoryService { get; }
        ILibraryService LibraryService { get; }
        IMarketService MarketService { get; }
        IOrderService OrderService { get; }
        IProductService ProductService { get; }
        IRefundService RefundService { get; }
        IUserService UserService { get; }
        IMarketTransactionService MarketTransactionService { get; }
    }
}
