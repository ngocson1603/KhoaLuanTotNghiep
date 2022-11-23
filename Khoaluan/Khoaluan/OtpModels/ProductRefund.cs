using System;

namespace Khoaluan.OtpModels
{
    public class ProductRefund
    {
        public int userId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }    
        public DateTime DatePurchase { get; set; }
    }
}
