using Khoaluan.Models;
using System.Collections.Generic;

namespace Khoaluan.OtpModels
{
    public class TransactionModel
    {
        public Market Market { get; set; }
        public Users Buyer { get; set; }
        public Users Seller { get; set; }
        public MarketTransaction MarketTransaction { get; set; }
        public Inventory BuyerInventory { get; set; }    
        public Inventory SellerInventory { get; set; }
    }
    public class TransactionRequest
    {
        public int MarketID { get; set; }
        public int buyerID { get; set; }    
        public int sellerID { get; set; }
        public int itemID { get; set; } 
        public int quantity { get; set; }
        public int totalprice { get; set; }

    }

    public class MarketOrder
    {
        public List<Market> market { get; set; }
        public List<Order> order { get; set; }
        public List<MarketTransaction> marketbuy { get; set; }
        public List<AddFundTransaction> addFundTransaction { get; set; }
    }
}
