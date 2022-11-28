using System;

namespace Khoaluan.Models
{
    public class MarketTransaction:BaseEntity
    {
        public int MarketID { get; set; }
        public int BuyerID { get; set; }
        public int SellerID { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public DateTime DateTransaction { get; set; }
        public Users Buyer { get; set; }    
        public Users Seller { get; set; }
        public Item Item { get; set; }
        public Market Market { get; set; }
    }
}
