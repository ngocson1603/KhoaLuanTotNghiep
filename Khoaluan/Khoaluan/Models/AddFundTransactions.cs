using System;
using System.Web;

namespace Khoaluan.Models
{
    public class AddFundTransaction
    {
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public int FundId { get; set; }
        public int UserId { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CreateOn { get; set; }
        public Users User { get; set; }
        public Fund Fund { get; set; }
    }
}
