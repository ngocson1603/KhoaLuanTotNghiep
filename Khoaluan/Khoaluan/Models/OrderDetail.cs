namespace Khoaluan.Models
{
    public class OrderDetail:BaseEntity
    {
        public int ProductID { get; set; }
        public int Price { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
