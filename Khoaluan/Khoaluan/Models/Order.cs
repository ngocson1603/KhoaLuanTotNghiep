using System;
using System.Collections.Generic;

namespace Khoaluan.Models
{
    public class Order:BaseEntity
    {
        public int UserID { get; set; }
        public DateTime DatePurchase { get; set; }  
        public int TotalPrice { get; set; }
        public Users User { get; set; }
        public List<OrderDetail>OrderDetails { get; set; }
        public List<Refund> Refunds { get; set; }
    }
}
