namespace Khoaluan.Models
{
    public class OrderDetail:BaseEntity
    {
        public int ProductID { get; set; }
        public decimal Price { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
