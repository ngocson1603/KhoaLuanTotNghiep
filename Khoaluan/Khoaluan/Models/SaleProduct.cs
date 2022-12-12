namespace Khoaluan.Models
{
    public class SaleProduct:BaseEntity
    {
        public int SaleID { get; set; }
        public int ProductID { get; set; }
        public int Discount { get; set; }
        public Sale Sale { get; set; }
        public Product Product { get; set; }
    }
}
