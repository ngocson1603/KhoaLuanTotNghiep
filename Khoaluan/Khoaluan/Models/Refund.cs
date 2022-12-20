using System;

namespace Khoaluan.Models
{
    public class Refund:BaseEntity
    {
        public int UserID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public DateTime DatePurchase { get; set; }
        public DateTime DateCreate { get; set; }
        public Users User { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
