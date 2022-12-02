using Khoaluan.Enums;
using Khoaluan.Interfaces;
using Khoaluan.InterfacesService;
using Khoaluan.Models;
using Khoaluan.OtpModels;
using System;
using System.Security.AccessControl;

namespace Khoaluan.Services
{
    public class MarketTransactionService : IMarketTransactionService
    {
        private readonly IMarketRepository _marketRepository;
        private readonly IUserService _userService;
        private readonly IInventoryService _inventoryService;
        public MarketTransactionService(IMarketRepository marketRepository, IUserService userService,IInventoryService inventoryService)
        {
            _marketRepository = marketRepository;
            _userService = userService;
            _inventoryService = inventoryService;
        }

        public MarketTransaction Transaction(TransactionRequest request)
        {
            MarketTransaction transaction = new MarketTransaction()
            {
                MarketID=request.MarketID,
                BuyerID=request.buyerID,
                SellerID=request.sellerID,
                ItemID=request.itemID,
                Quantity=request.quantity,
                TotalPrice=request.totalprice,
                DateTransaction=DateTime.Now
            };
            return transaction;
        }
    }
}
