using Khoaluan.Interfaces;
using Khoaluan.InterfacesRepository;
using Microsoft.EntityFrameworkCore;
using System;

namespace Khoaluan
{
    public interface IUnitOfWork:IDisposable
    {
        DbContext Context { get; }
        void SaveChange();
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IDeveloperRepository DeveloperRepository { get; }   
        IProductCategoryRepository ProductCategoryRepository { get; }
        IUsersRepository UserRepository { get; }
        ILibraryRepository LibraryRepository { get; }
        IOrderRepository OrderRepository { get; }
        IInventoryRepository InventoryRepository { get; }
        IMarketRepository MarketRepository { get; }
        IRefundRepository RefundRepository { get; }
        IAdminRepository AdminRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IItemRepository ItemRepository { get; }
        IDiscussionRepository DiscussionRepository { get; }
        IFundRepository FundRepository { get; }
        IMarketTransactionRepository MarketTransactionRepository { get; }
        ISaleRepository SaleRepository { get; }
        ISaleProductRepository SaleProductRepository { get; }
        IBlogRepository BlogRepository { get; }       
    }
}
