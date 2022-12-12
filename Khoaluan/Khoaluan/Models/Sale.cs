using System;
using System.Collections.Generic;

namespace Khoaluan.Models
{
    public class Sale:BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<SaleProduct> SaleProducts { get; set; }
    }
}
