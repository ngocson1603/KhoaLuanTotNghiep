using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khoaluan.ModelViews
{
    public class SaleModelView
    {
        public int SaleId { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public string Image { get; set; }
        public string SaleName { get; set; }
        public int Id { get; set; }
        public int Discount { get; set; }
        public int Status { get; set; }
        public decimal Price { get; set; }
        public string NameSale { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}
