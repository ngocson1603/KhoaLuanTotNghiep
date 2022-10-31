namespace Khoaluan.Models
{
    public class Library
    {
        public int UserId { get; set; } 
        public int ProductId { get; set; }
        public virtual Users Users { get; set; }
        public virtual Product Product { get; set; }
    }
}
