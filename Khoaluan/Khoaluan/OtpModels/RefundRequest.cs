using System;

namespace Khoaluan.OtpModels
{
    public class RefundRequest
    {
        public int OrderId { get; set; }
        public int Price { get; set; }
        public DateTime DatePurchase { get; set; }
    }
}
