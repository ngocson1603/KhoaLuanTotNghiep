using Khoaluan.Models;
using System;

namespace Khoaluan.OtpModels
{
    public class RefundRequest
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public decimal Price { get; set; }
        public DateTime DatePurchase { get; set; }
    }
    public class RefundUser
    {
        public int Id { get; set; }
        public int UserID { get; set; } 
        public decimal Price { get; set; }
    }
}
