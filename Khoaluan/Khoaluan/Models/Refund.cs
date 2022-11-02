namespace Khoaluan.Models
{
    public class Refund:BaseEntity
    {
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Status { get; set; }
        public Users User { get; set; }
        public Product Product { get; set; }
    }
}
