using System;
using System.Collections.Generic;

namespace Khoaluan.Models
{
    public class Market:BaseEntity
    {
        public int UserID { get;set; }
        public int ItemID { get; set; }
        public decimal PricePerItem { get; set; }   
        public DateTime DayCreate { get; set; }
        public int Status { get; set; }
        public int Quantity { get; set; }
        public Users User { get; set; }
        public Item Item { get; set; }
        public List<MarketTransaction> MarketTransactions { get; set; }
    }
}
