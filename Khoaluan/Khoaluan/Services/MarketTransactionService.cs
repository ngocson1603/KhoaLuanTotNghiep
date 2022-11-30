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

        public TransactionModel Transaction(TransactionRequest request)
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
            Users buyer = _userService.updateBalance(request.buyerID, request.totalprice, (int)marketType.buy);
            Users seller=_userService.updateBalance(request.sellerID,request.totalprice,(int)marketType.sell);
            Market market=_marketRepository.GetById(request.MarketID);
            market.Quantity = market.Quantity - request.quantity;
            if(market.Status==(int)marketType.sell)
            {
                if(market.Quantity==0)
                {
                    market.Status = (int)marketType.soldout;
                }
            }
            else if(market.Status==(int)marketType.order)
            {
                if(market.Quantity==0)
                {
                    market.Status = (int)marketType.purchased;
                }
            }
            Inventory buyerInventory = _inventoryService.updateInventory(request.buyerID,request.itemID, (int)marketType.buy,request.quantity);
            Inventory sellerInventory = _inventoryService.updateInventory(request.sellerID, request.itemID, (int)marketType.sell,request.quantity);
            TransactionModel model = new TransactionModel()
            {
                Market = market,
                Buyer = buyer,
                Seller=seller,
                BuyerInventory=buyerInventory,
                SellerInventory=sellerInventory,
            };
            return model;
        }
    }
}
