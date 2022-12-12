using System;
using System.Collections.Generic;

namespace Khoaluan.OtpModels
{
    public class SaleRequest
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<SaleProductRequest> SaleProductRequests { get; set; }
    }
    public class SaleProductRequest
    {
        public int ProductID { get; set; }
        public int Discount { get; set; }
    }
}
